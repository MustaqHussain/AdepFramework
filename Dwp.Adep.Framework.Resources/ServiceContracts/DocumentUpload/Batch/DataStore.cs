using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    public class DataStore : IDataStore
    {
        private DbProviderFactory factory;

        private const string RETURN_VALUE_PARAM = "@return_value";

        #region constructor
        public DataStore(DbProviderFactory factory_)
        {
            this.factory = factory_;
        }
        #endregion

        public int RunProcedure(String procName, String connectionString)
        {
            using (SqlConnection connection = (SqlConnection)factory.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                  
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procName;
                        SqlParameter parameter = new SqlParameter(RETURN_VALUE_PARAM, DbType.Int32);
                        parameter.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(parameter);

                        connection.Open();

                        command.ExecuteNonQuery();

                        string result = command.Parameters[RETURN_VALUE_PARAM].Value.ToString();

                        return int.Parse(result);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<String> GetColumnNames(String connectionString, String tableName)
        {
            List<string> databaseColumns = new List<string>();
            using (SqlConnection connection = (SqlConnection)factory.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    // Interrogate the table for its columns - assuming same order as spreadsheet
                    string[] restrictionValues = new string[4];
                    restrictionValues[2] = tableName;
                    restrictionValues[1] = "dbo";
                    DataTable schema = connection.GetSchema("Columns", restrictionValues);


                    foreach (DataRow row in schema.Rows)
                    {
                        databaseColumns.Add(row["COLUMN_NAME"].ToString());
                    }
                }                
                finally
                {
                    connection.Close();
                }
            }
            return databaseColumns;
        }

        public void Save(DataTable dataTable, String connectionString, String tableName, List<String> columns, List<String> databaseColumns)
        {

            using (SqlConnection connection = (SqlConnection)factory.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        for (int i = 0; i < columns.Count(); i++)
                        {
                            bulkCopy.ColumnMappings.Add(columns[i], databaseColumns[i]);
                        }

                        bulkCopy.WriteToServer(dataTable);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}