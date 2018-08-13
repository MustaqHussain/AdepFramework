using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.DataContracts
{
    [DataContract]
    public class BatchDetails
    {

        #region constructors
        /// <summary>
        /// Creates a batch details object with all the required information to run the batch job. App ID is set later once created.
        /// </summary>
        /// <param name="appDbConnectionString"></param>
        /// <param name="fileToUpload"></param>
        /// <param name="tableName"></param>
        /// <param name="appId"></param>
        /// <param name="storedProcToRun"></param>
        public BatchDetails(String appDbConnectionString_, String fileToUpload_, String tableName_, Guid appId_, String storedProcToRun_, String currentUser_)
        {
            DatabaseConnectionString = appDbConnectionString_;
            FileName = fileToUpload_;
            TableName = tableName_;
            AppId = appId_;
            StoredProcedure = storedProcToRun_;
            CurrentUser = currentUser_;
        }

        #endregion

        [DataMember]
        public String CurrentUser { get; private set; }

        [DataMember]
        public String DatabaseConnectionString { get; private set; }

        [DataMember]
        public String FileName { get; private set; }

        [DataMember]
        public String TableName { get; private set; }

        [DataMember]
        public Guid ProcessId { get; set; }

        [DataMember]
        public Guid AppId { get; private set; }

        [DataMember]
        public String StoredProcedure { get; private set; }

        [DataMember]
        // If set, this action method will be called by the worker thread. Can allow some custom validation on the batch if requierd.
        public Action PreProcessing { get; set; }

        [DataMember]
        public Action PostProcessing { get; set; }
    }

}