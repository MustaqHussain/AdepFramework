using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;

using System.Data.Common;
namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    public class DataStoreFactory : IDataStoreFactory
    {

        /// <summary>
        /// Allows direct database access for SQL bulk copy and execution of procedure
        /// </summary>
        private readonly DbProviderFactory factory;

        #region constructors
        public DataStoreFactory()
        {
            ConnectionStringSettings dataProviderSettings = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["DataProvider"]];

            if (dataProviderSettings == null)
            {
                throw new UploadConfigurationException(String.Format("DataProvider app settings {0} not valid", ConfigurationManager.AppSettings["DataProvider"]));
            }

            factory = DbProviderFactories.GetFactory(dataProviderSettings.ProviderName);
        }

        public DataStoreFactory(DbProviderFactory factory_)
        {
            factory = factory_;
        }

        #endregion

        public IDataStore CreateDataStore()
        {

            return new DataStore(factory);
        }
    }
}