using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Dwp.Adep.Framework.Resources.FaultContracts;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBatchUploadService" in both code and config file together.
    [ServiceContract]    
    public interface IBatchUploadService
    {

        /// <summary>
        /// Upload a file to a temporary location from where it can be copied. Return temporary location name, used to trigger batch jobs.
        /// </summary>
        /// <param name="documentLocation"></param>
        /// <param name="tempDirectory"></param>
        /// <param name="replacementFileName"></param>
        /// <returns></returns>
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        String Upload(String documentLocation);


        /// <summary>
        /// Log a new upload record in the database as queued
        /// </summary>
        /// <param name="batchDetails"></param>
        /// <returns></returns>
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        String QueueBatch(BatchDetails batchDetails);

        /// <summary>
        /// Get percentage complete as calculated by the underlying RunWorker method
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        int GetPercentComplete(Guid processId);

        /// <summary>
        /// For a record in state FAILED, get further information
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="userId"></param>
        /// <returns>Message from the exception thrown during failure</returns>
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        String GetErrorMessage(Guid processId, string userId);

        /// <summary>
        /// Attempt to cancel a batch. Returns a SOAP fault for invalid ID
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="userId"></param>
        /// <returns>CANCELLED if the cancellateion request was sent successfully, COMPLETE if the process has already run</returns>        
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        UploadStatus CancelBatch(Guid processId, String userId);

        /// <summary>
        /// Return the status a process ID is in. Returns a SOAP fault for invalid ID
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        UploadStatus CheckStatus(Guid processId, String user);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void ProcessAll(String user);
    }
}
