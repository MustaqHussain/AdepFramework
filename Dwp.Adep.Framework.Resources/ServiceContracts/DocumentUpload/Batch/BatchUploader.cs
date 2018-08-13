using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Data;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    class BatchUploader
    {

        #region properties
        private BackgroundWorker batchUploader;
       
        private IDocumentUploadService documentUploadService;
        private IDataStore dataStore;

        public BackgroundWorker GetBatchUploader()
        {
            return batchUploader;
        }

        public UploadStatus Status { get; private set; }

        public int Percentage { get; private set; }

        public BatchDetails BatchDetails { get; private set; }

        #endregion

        #region constructors
        /// <summary>
        /// Creates a BatchUploader and starts the background worker thread using the given batch details
        /// </summary>
        /// <param name="worker">Background thread to start</param>
        /// <param name="batchDetails">Database and file details required to do work</param>
        public BatchUploader(BackgroundWorker worker_, BatchDetails batchDetails_, IDataStore dataStore_, IDocumentUploadService documentUploadService_)
        {
            batchUploader = worker_;
            BatchDetails = batchDetails_;
            dataStore = dataStore_;
            documentUploadService = documentUploadService_;

            batchUploader.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);

            batchUploader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);

            batchUploader.DoWork += new DoWorkEventHandler(DoWork);

            batchUploader.WorkerReportsProgress = true;
            batchUploader.WorkerSupportsCancellation = true;

            Percentage = 0;
            batchUploader.RunWorkerAsync();

        }
        #endregion

        #region BackgroundWorker methods

        void ProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            Percentage = args.ProgressPercentage;
        }

        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            // Clean up - delete file
            documentUploadService.DeleteFile(BatchDetails.FileName);
                        
        }

        void DoWork(object sender, DoWorkEventArgs args)
        {
          
                batchUploader.ReportProgress(1);

                // TODO stuff, check for cancellation in breaks
                IFileWrapper fileWrapper;
                DataTable dataTable;

                try
                {
                    fileWrapper = DocumentWrappingFactory.CreateDocumentWrapper(BatchDetails.FileName);
                    if (CheckCancellation(args))
                    {
                        return;
                    }
                    dataTable = fileWrapper.GetDataTable();
                    if (CheckCancellation(args))
                    {
                        return;
                    }

                    if (BatchDetails.PreProcessing != null)
                    {
                        batchUploader.ReportProgress(25);
                        BatchDetails.PreProcessing();
                    }
                }
                catch (Exception e)
                {
                    // Fail
                    UploadResult result = new UploadResult(BatchDetails.ProcessId, BatchDetails.CurrentUser);
                    args.Result = result;
                    result.Status = UploadStatus.Failed;
                    result.ErrorMessage = e.Message;

                    return;
                }
                batchUploader.ReportProgress(50);

                Upload(dataTable, fileWrapper.GetColumnNames(), args);

                RunProc(args);

                if (CheckCancellation(args))
                {
                    return;
                }

            // Post processing
                if (BatchDetails.PostProcessing != null)
                {
                    try
                    {

                        batchUploader.ReportProgress(75);
                        BatchDetails.PostProcessing();

                    }
                    catch (Exception e)
                    {
                        // Fail
                        UploadResult result = new UploadResult(BatchDetails.ProcessId, BatchDetails.CurrentUser);
                        args.Result = result;
                        result.Status = UploadStatus.Failed;
                        result.ErrorMessage = "Post Processing failed with exception " + e.Message;

                        return;
                    }
                }
                batchUploader.ReportProgress(100);                

        }

        private void RunProc(DoWorkEventArgs args)
        {
            UploadResult result = (UploadResult)args.Result;
            try
            {
                if (BatchDetails.StoredProcedure != null)
                {
                    int code = dataStore.RunProcedure(BatchDetails.StoredProcedure, BatchDetails.DatabaseConnectionString);

                    if (code == 1)
                    {
                        // Failure
                        result.Status = UploadStatus.Failed;
                        result.ErrorMessage = String.Format("Stored procedure {0} failed to run, exited with code 1", BatchDetails.StoredProcedure);
                        return;
                    }
                }
                result.Status = UploadStatus.Processed;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Status = UploadStatus.Failed;
            }
        }

        private void Upload(DataTable dataTable, List<string> columns, DoWorkEventArgs args)
        {
            UploadResult result = new UploadResult(BatchDetails.ProcessId, BatchDetails.CurrentUser);
            args.Result = result;
            try
            {
                // Interrogate the table for its columns - assuming same order as spreadsheet                    
                List<string> databaseColumns = dataStore.GetColumnNames(BatchDetails.DatabaseConnectionString, BatchDetails.TableName);

                if (columns.Count() != databaseColumns.Count())
                {
                    throw new UploadConfigurationException(String.Format("Upload table {0} column count must match file {1} column count. Found {2}, expected {3}",
                        BatchDetails.TableName, BatchDetails.FileName, databaseColumns.Count(), columns.Count()));
                }

                batchUploader.ReportProgress(70);

                dataStore.Save(dataTable, BatchDetails.DatabaseConnectionString, BatchDetails.TableName, columns, databaseColumns);

                result.Status = UploadStatus.Processed;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Status = UploadStatus.Failed;
            }

        }

        private bool CheckCancellation(DoWorkEventArgs args)
        {
            if (batchUploader.CancellationPending)
            {
                // Update status on result. BatchUploadService will update queue status.
                args.Cancel = true;

                batchUploader.ReportProgress(0);

                if (args.Result == null)
                {
                    // create one
                    args.Result = new UploadResult(BatchDetails.ProcessId, BatchDetails.CurrentUser);                   
                }

                UploadResult result = (UploadResult)args.Result;
                result.Status = UploadStatus.Cancelled;

                return true;
            }
            return false;
        }

        #endregion

    }
}
