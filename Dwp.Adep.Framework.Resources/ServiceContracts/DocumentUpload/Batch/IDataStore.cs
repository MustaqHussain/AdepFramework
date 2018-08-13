using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    /// <summary>
    /// Class representing the target storage for the data file copy
    /// </summary>
    public interface IDataStore
    {
        /// <summary>
        /// Get target columns to map to file
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<String> GetColumnNames(String connectionString, String tableName);

        /// <summary>
        /// Transfer file data to data store, using column mappings provided
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="columns">File columns</param>
        /// <param name="databaseColumns">Target store columns</param>
        void Save(DataTable dataTable, String connectionString, String tableName, List<String> columns, List<String> databaseColumns);

        /// <summary>
        /// Run a specific stored proc against the given connection string
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="connectionString"></param>
        /// <returns>result code - success or error or otherwise</returns>
        int RunProcedure(String procName, String connectionString);
    }
}
