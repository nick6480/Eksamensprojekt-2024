using System;

namespace SQLTest5.Modules
{
    // Provides high-level database operations using an SQLQueryExecutor.
    public class DatabaseService
    {
        private readonly SQLQueryExecutor _queryExecutor;

        public DatabaseService(SQLQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        // Fetches and prints data from a specified database table.
        public void GetDataFromTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

            string sql = $"SELECT * FROM dbo.{tableName}";  // SQL query to select all records from the table.
            _queryExecutor.ExecuteQueryAndPrintResults(sql);  // Execute the query and print results.
        }
    }
}
