using System;
using Microsoft.Data.SqlClient;

namespace TestAfAlt.Modules
{
    // Implements operations that can be performed on a database
    public class DatabaseService : IDatabaseOperations
    {
        private readonly SQLQueryExecutor _queryExecutor;

        public DatabaseService(SQLQueryExecutor queryExecutor)
        {
            // Ensure the query executor is not null
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        // Fetches and prints data from a specified database table.
        public void GetDataFromTable(string tableName)
        {
            Console.WriteLine("We are here 11");
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));
            }

            // SQL query to select all records from the specified table
            string sql = $"SELECT * FROM dbo.{tableName}";
            // Execute the query and print results
            _queryExecutor.ExecuteQueryAndPrintResults(sql);
        }

        // Executes a stored procedure with the given name and parameters
        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            Console.WriteLine("We are here 22");
            if (string.IsNullOrWhiteSpace(procedureName))
            {
                throw new ArgumentException("Procedure name cannot be null or empty.", nameof(procedureName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");
            }

            // Execute the stored procedure using the query executor
            _queryExecutor.ExecuteStoredProcedure(procedureName, parameters);
        }

        // Executes a SQL scalar function and returns the result
        public object ExecuteScalarFunction(string functionName, SqlParameter[] parameters)
        {
            Console.WriteLine("We are here 33");
            if (string.IsNullOrWhiteSpace(functionName))
            {
                throw new ArgumentException("Function name cannot be null or empty.", nameof(functionName));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters), "Parameters cannot be null.");
            }

            // Execute the scalar function and return the result
            return _queryExecutor.ExecuteScalarFunction(functionName, parameters);
        }
    }
}
