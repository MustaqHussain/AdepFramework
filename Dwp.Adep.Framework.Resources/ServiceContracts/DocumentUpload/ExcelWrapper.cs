using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    class ColumnMapping
    {
        public ColumnMapping(String excelName, String databaseName)
        {
            ExcelName = excelName;
            DatabaseName = databaseName;
        }
        String ExcelName { get; set; }
        String DatabaseName { get; set; }
    }

    public class ExcelWrapper : IFileWrapper
    {
        private string excelConnectionString;
        private DbProviderFactory factory;

        public String SheetName { get; private set; }

        private List<String> Exceptions { get; set; }

        public ExcelWrapper(string fileName)
        {
            excelConnectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1""", fileName);
            factory = DbProviderFactories.GetFactory("System.Data.OleDb");

            Exceptions = new List<String>();

            SheetName = GetSheetName();
        }

        public void AddException(String columnName)
        {
            Exceptions.Add(columnName);
        }

        /// <summary>
        /// Finds the name of the first tab in a worksheet
        /// </summary>
        /// <returns>Sheet name</returns>
        public string GetSheetName()
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            OleDbConnection connection = new OleDbConnection(excelConnectionString);
            connection.Open();

            DataTable schema = new DataTable();
            schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string sheetName = schema.Rows[0]["TABLE_NAME"].ToString();

            connection.Close();
            return sheetName;
        }

        /// <summary>
        /// Find a list of column names in the specified sheet
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public List<string> GetColumnNames()
        {
            DbDataAdapter adapter = factory.CreateDataAdapter();
            OleDbConnection connection = new OleDbConnection(excelConnectionString);
            connection.Open();

            DataTable schema = new DataTable();

            schema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, SheetName, null });
            connection.Close();

            List<string> columns = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                String nextName = row["COLUMN_NAME"].ToString();
                if (!Exceptions.Contains(nextName))
                {
                    columns.Add(nextName);
                }
            }

            return columns;
        }

        /// <summary>
        /// Read data from the specified sheet
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            List<string> columns = GetColumnNames();

            DataTable dataTable = new DataTable();

            DbDataAdapter adapter = factory.CreateDataAdapter();
            OleDbConnection connection = new OleDbConnection(excelConnectionString);
            connection.Open();

            DbCommand command = factory.CreateCommand();
            string selectStatement = "";
            for (int i = 0; i < columns.Count(); i++)
            {
                selectStatement += String.Format("LTRIM(RTRIM([{0}])) AS [{0}]", columns[i]); 
                if (i < (columns.Count() - 1))
                {
                    selectStatement += ", ";
                }
            }

            command.CommandText = String.Format("SELECT {0} FROM [{1}]", selectStatement, SheetName);
            command.Connection = connection;
            adapter.SelectCommand = command;

            try
            {
                adapter.Fill(dataTable);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }

    }
}
