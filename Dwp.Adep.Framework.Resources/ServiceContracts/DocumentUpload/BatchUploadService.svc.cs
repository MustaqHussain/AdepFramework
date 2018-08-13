using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ComponentModel;

using Dwp.Adep.Framework.Resources.DataContracts;
using Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch;

using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BatchUploadService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BatchUploadService : IBatchUploadService
    {
        /// <summary>
        /// Shared list of running threads
        /// </summary>
        private static Dictionary<Guid, BatchUploader> batchUploaders = new Dictionary<Guid, BatchUploader>();

        /// <summary>
        /// Allows files to be deleted after completion of batch upload to database
        /// </summary>
        private IDocumentUploadService documentUploadService;

        /// <summary>
        /// Allows injection of batch uploader
        /// </summary>
        private IDataStoreFactory dataStoreFactory;

        /// <summary>
        /// Manages the database queue
        /// </summary>
        private QueueManager queueManager;

        /// <summary>
        /// Manages multithreaded access to the list of running threads
        /// </summary>
        private static object batchUploadLock = new object();

        private IExceptionHandler exceptionHandler;

        #region Constructor
        public BatchUploadService()
        {

            documentUploadService = new DocumentUploadService();
            queueManager = new QueueManager();
            exceptionHandler = new ExceptionHandler();
            dataStoreFactory = new DataStoreFactory();
        }

        public BatchUploadService(IDocumentUploadService uploadService_, QueueManager queueManager_, IExceptionHandler exceptionHandler_, IDataStoreFactory batchUploader_)
        {

            documentUploadService = uploadService_;
            queueManager = queueManager_;
            exceptionHandler = exceptionHandler_;
            dataStoreFactory = batchUploader_;

        }

        #endregion

        #region private and protected methods
        private Guid PersistQueue(BatchDetails batchDetails)
        {
            return queueManager.Queue(batchDetails);
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            // Clean up - remove from queue, set status
            UploadResult result = (UploadResult)args.Result;

            lock (batchUploadLock)
            {
                batchUploaders.Remove(result.ProcessId);
            }

            if (result.Status == UploadStatus.Processed)
            {
                // Dequeue
                queueManager.Dequeue(result.ProcessId, result.UserId);

            }
            else if (result.Status == UploadStatus.Failed)
            {
                //  we can't throw an exception here as we are on a callback thread. But we have an error message.
                //  put it in an error table for query
                String errorMessage = result.ErrorMessage;
                queueManager.AddErrorMessage(result.ProcessId, errorMessage, result.UserId);
            }
            else
            {
                // This should also catch cancelled events
                queueManager.UpdateQueue(result.ProcessId, result.Status, result.UserId);
            }

        }

        private void CheckUniqueRun(BatchDetails batchDetails)
        {

            lock (batchUploadLock)
            {
                foreach (BatchUploader runner in batchUploaders.Values)
                {
                    // Case where we can't start another
                    if (runner.BatchDetails.FileName.Equals(batchDetails.FileName))
                    {
                        throw new UploadConfigurationException(String.Format("A request for batch {0} is already queued", batchDetails.FileName));
                    }
                }
            }
        }

        private void CheckFileExists(BatchDetails batchDetails)
        {
            if (!documentUploadService.FileExists(batchDetails.FileName))
            {
                if (batchDetails.ProcessId != null)
                {
                    // Get rid of this record
                    queueManager.UpdateQueue(batchDetails.ProcessId, UploadStatus.Failed, batchDetails.CurrentUser);
                    queueManager.AddErrorMessage(batchDetails.ProcessId, String.Format("Batch {0} cannot run. File {0} has been deleted", batchDetails.ProcessId, batchDetails.FileName), batchDetails.CurrentUser);
                }
                throw new UploadConfigurationException(String.Format("File {0} for batch {1} does not exist", batchDetails.FileName, batchDetails.ProcessId));
            }
        }


        private IEnumerable<Guid> GetProcessesToRun(String userId)
        {
            List<Guid> idsInRunningOrPending = queueManager.GetItems(UploadStatus.Queued, userId);
            idsInRunningOrPending.AddRange(queueManager.GetItems(UploadStatus.Running, userId));

            IEnumerable<Guid> listedAsRunningButNotRunning = null;

            lock (batchUploadLock)
            {
                listedAsRunningButNotRunning = from c in idsInRunningOrPending
                                               where !(from running in batchUploaders.Values
                                                       select running.BatchDetails.ProcessId).Contains(c)
                                               select c;

            }
            return listedAsRunningButNotRunning;
        }

        private BatchDetails GetBatchDetails(Guid processId, String user)
        {
            BatchDetails item = queueManager.GetBatchDetails(processId, user);

            if (item == null)
            {
                exceptionHandler.ShieldException(new ArgumentOutOfRangeException(String.Format("Batch with ID {0} does not exist", processId)));
            }

            return item;
        }

        #endregion

        #region interface methods

        public String Upload(String documentLocation)
        {

            try
            {
                // We are assuming that the file will be in a directory - safe assumption. Otherwise fault will be thrown.
                String replacementFileName = documentLocation.Substring(documentLocation.LastIndexOf("\\"));
                
                documentUploadService.UploadFile(documentLocation, replacementFileName);
            }
            catch (Exception ex)
            {
                exceptionHandler.ShieldException(ex);
            }
            return null;
        }

        public String QueueBatch(BatchDetails batchDetails)
        {
            try
            {
                // Make sure no other processing running on this file name
                CheckUniqueRun(batchDetails);

                // Check that the temp file exists
                CheckFileExists(batchDetails);

                // Save new details row in database, generate GUID into batch details
                Guid processId = PersistQueue(batchDetails);

                // Add completion callback to batchUploader
                BackgroundWorker batchUploader = new BackgroundWorker();
                batchUploader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

                batchUploaders.Add(batchDetails.ProcessId, new BatchUploader(batchUploader, batchDetails, dataStoreFactory.CreateDataStore(), documentUploadService)); // NB this starts the background thread

                return processId.ToString();
            }
            catch (Exception e)
            {
                exceptionHandler.ShieldException(e);
                return null;
            }
        }

        public int GetPercentComplete(Guid processId)
        {

            BatchUploader wrapper = null;

            if (batchUploaders.TryGetValue(processId, out wrapper))
            {
                return wrapper.Percentage;
            }


            exceptionHandler.ShieldException(new ArgumentOutOfRangeException(String.Format("No process with ID {0)", processId)));
            return 0;
        }

        public UploadStatus CancelBatch(Guid processId, String userId)
        {
            BatchUploader wrapper = null;

            if (batchUploaders.TryGetValue(processId, out wrapper))
            {
                BackgroundWorker batchUploader = wrapper.GetBatchUploader();
                if (batchUploader.IsBusy)
                {

                    batchUploader.CancelAsync();

                    return UploadStatus.Cancelled;
                }
                return UploadStatus.Processed;
            }
            // Message contract
            exceptionHandler.ShieldException(new ArgumentOutOfRangeException(String.Format("No process with ID {0)", processId)));
            return UploadStatus.Invalid;
        }

        public UploadStatus CheckStatus(Guid processId, String user)
        {
            BatchUploader wrapper = null;

            if (batchUploaders.TryGetValue(processId, out wrapper))
            {
                BackgroundWorker batchUploader = wrapper.GetBatchUploader();
                if (batchUploader.IsBusy)
                {
                    return UploadStatus.Running;
                }
            }

            return queueManager.GetStatus(processId, user);
        }

        public String GetErrorMessage(Guid processId, String user)
        {
            String exception = queueManager.GetErrorMessage(processId, user);

            if (exception == null)
            {
                exceptionHandler.ShieldException(new ArgumentOutOfRangeException(String.Format("No error message for process with ID {0)", processId)));
            }

            return exception;
        }

        public void ProcessAll(String user)
        {
            foreach (Guid guid in GetProcessesToRun(user))
            {
                QueueBatch(GetBatchDetails(guid, user));
            }
        }

        #endregion

        public int GetQueueSize()
        {
            return batchUploaders.Count;
        }
    }
}
