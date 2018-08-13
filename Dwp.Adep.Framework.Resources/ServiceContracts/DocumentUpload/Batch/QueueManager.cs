using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Resources.AdminService;
using Dwp.Adep.Framework.Resources.DataContracts;


namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    public class QueueManager
    {
        private IAdminService uploadQueueManager;

        public string appID = "FrameworkAdmin";

        public QueueManager() : this(new AdminServiceClient()) {}

        public QueueManager(IAdminService adminServiceClient)
        {
            uploadQueueManager = adminServiceClient;
        }

        public Guid Queue(BatchDetails batchDetails)
        {
            UploadQueueDC dc = new UploadQueueDC();
           
            dc.ApplicationCode = batchDetails.AppId;
            dc.ConnectionString = batchDetails.DatabaseConnectionString;
            dc.TempTableName = batchDetails.TableName;
            dc.StoredProcedure = batchDetails.StoredProcedure;
            dc.UploadFileName = batchDetails.FileName;
            dc.Status = "R";

            UploadQueueVMDC result = uploadQueueManager.CreateUploadQueue(batchDetails.CurrentUser, batchDetails.CurrentUser, appID, "", dc);

            batchDetails.ProcessId = result.UploadQueueItem.Code;

            return result.UploadQueueItem.Code;
        }

        public UploadStatus GetStatus(Guid batchDetailsId, String user)
        {
            UploadQueueVMDC uploadRecord = uploadQueueManager.GetUploadQueue(user, user, appID, "", batchDetailsId.ToString());

            UploadStatus status = UploadStatus.Invalid;

            if (uploadRecord == null || uploadRecord.UploadQueueItem == null)
            {
                return status;
            }

            switch (uploadRecord.UploadQueueItem.Status)
            {
                case "X":
                    status = UploadStatus.Cancelled;
                    break;
                case "P":
                    status = UploadStatus.Processed;
                    break;
                case "F":
                    status = UploadStatus.Failed;
                    break;
                case "R":
                    status = UploadStatus.Running;
                    break;
                case "Q":
                    status = UploadStatus.Queued;
                    break;
                case "U":
                    status = UploadStatus.Uploaded;
                    break;
            }
            return status;
        }

        public void UpdateQueue(Guid batchDetailsId, UploadStatus status, String user)
        {
            UploadQueueVMDC uploadRecord = uploadQueueManager.GetUploadQueue(user, user, appID, "", batchDetailsId.ToString());

            UploadQueueDC toUpdate = uploadRecord.UploadQueueItem;

            switch (status)
            {
                case UploadStatus.Cancelled:
                    toUpdate.Status = "X";
                    break;
                case UploadStatus.Uploaded:
                    toUpdate.Status = "U";
                    break;
                case UploadStatus.Processed:
                    toUpdate.Status = "P";
                    break;
                case UploadStatus.Failed:
                    toUpdate.Status = "F";
                    break;
                case UploadStatus.Queued:
                    toUpdate.Status = "Q";
                    break;
                case UploadStatus.Running:
                    toUpdate.Status = "R";
                    break;
            }
            uploadQueueManager.UpdateUploadQueue(user, user, appID, "", toUpdate);
        }

        public List<Guid> GetItems(UploadStatus status, String user)
        {

            UploadQueueSearchCriteriaDC query = new UploadQueueSearchCriteriaDC();

            switch (status)
            {
                case UploadStatus.Cancelled:
                    query.Status = "X";
                    break;
                case UploadStatus.Processed:
                    query.Status = "C";
                    break;
                case UploadStatus.Failed:
                    query.Status = "F";
                    break;
                case UploadStatus.Queued:
                    query.Status = "Q";
                    break;
                case UploadStatus.Running:
                    query.Status = "R";
                    break;
            }

            UploadQueueSearchVMDC results = uploadQueueManager.SearchUploadQueue(user, user, appID, "", query, 1, 100, true);

            List<Guid> itemIds = new List<Guid>(results.RecordCount);

            if (results.RecordCount > 0)
            {
                foreach (UploadQueueSearchMatchDC result in results.MatchList)
                {
                    itemIds.Add(result.Code);
                }
            }
            return itemIds;
        }

        public BatchDetails GetBatchDetails(Guid batchDetailsId, String user)
        {
            UploadQueueVMDC uploadRecord = uploadQueueManager.GetUploadQueue(user, user, appID, "", batchDetailsId.ToString());

            if (uploadRecord != null)
            {
                // TODO use AutoMapper and VMDC object here?? What does a BatchDetails give me?
                BatchDetails batchDetails = new BatchDetails(uploadRecord.UploadQueueItem.ConnectionString, 
                    uploadRecord.UploadQueueItem.UploadFileName, 
                    uploadRecord.UploadQueueItem.TempTableName, 
                    batchDetailsId, 
                    uploadRecord.UploadQueueItem.StoredProcedure, 
                    user);
                return batchDetails;
            }
            return null;
        }

        public void Dequeue(Guid batchDetailsId, String user)
        {
            // TODO what is dequeue? Mark as complete?
            UpdateQueue(batchDetailsId, UploadStatus.Processed, user);
        }

        public void Delete(Guid batchDetailsId, String user)
        {
            UploadQueueVMDC uploadRecord = uploadQueueManager.GetUploadQueue(user, user, appID, "", batchDetailsId.ToString());

            if (uploadRecord != null)
            {
                uploadQueueManager.DeleteUploadQueue(user, user, appID, "", batchDetailsId.ToString(), uploadRecord.UploadQueueItem.RowIdentifier.ToString());
            }
        }

        /// <summary>
        /// Insert the error into a table for future reference
        /// </summary>
        /// <param name="batchDetailsId"></param>
        /// <param name="errorMessage"></param>
        /// <param name="user"></param>
        public void AddErrorMessage(Guid batchDetailsId, String errorMessage, String user)
        {
            
            UploadErrorLogDC dc = new UploadErrorLogDC();            
            dc.UploadCode = batchDetailsId;
            dc.ErrorMessage = errorMessage;

            uploadQueueManager.CreateUploadErrorLog(user, user, appID, "", dc);
        }

        public String GetErrorMessage(Guid batchDetailsId, String user)
        {
            UploadErrorLogVMDC vmdc = uploadQueueManager.GetUploadErrorLog(user, user, appID, "", batchDetailsId.ToString());

            if (vmdc != null && vmdc.UploadErrorLogItem != null)
            {
                return vmdc.UploadErrorLogItem.ErrorMessage;
            }

            // TODO exception or null?
            return null;
        }
    }
}