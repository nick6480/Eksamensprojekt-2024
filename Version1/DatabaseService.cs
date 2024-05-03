using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQLTest2
{
    public class DatabaseService
    {
        // Holds a reference to SQLQueryExecutor.
        private SQLQueryExecutor _queryExecutor;

        public DatabaseService(SQLQueryExecutor queryExecutor)
        {
            // Initializes the query executor in the constructor.
            _queryExecutor = queryExecutor;
        }

        public void GetDataFromTable(string tableName)
        {
            // SQL query to retrieve all records from the specified table.
            string sql = $"SELECT * FROM dbo.{tableName}";

            // Executes the SQL query and prints the results using queryExecutor.
            _queryExecutor.ExecuteQueryAndPrintResults(sql);
        }
    }

}
