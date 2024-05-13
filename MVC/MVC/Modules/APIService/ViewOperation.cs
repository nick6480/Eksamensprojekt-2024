using System;
using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using MVC.Modules.DatabaseAdgang;

namespace MVC.Modules.APIService
{
    public class ViewOperation : IDatabaseOperation
    {
        private readonly SQLQueryExecutor _queryExecutor;

        public ViewOperation(SQLQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        // Async method to execute a view query and retrieve results
        public async Task<string> ExecuteAsync(string viewQuery, SqlParameter[]? parameters = null)
        {
            // Assuming ExecuteQueryAsync returns a string with the results for simplicity
            string results = await _queryExecutor.ExecuteQueryAsync(viewQuery, parameters);
            LogOperation("Executed view query.");
            return results;
        }

        // Getter method to log and retrieve operation status
        private void LogOperation(string message)
        {
            // Log the operation message
            Console.WriteLine(message);
        }
        public void Execute(string viewQuery, SqlParameter[]? parameters = null)
        {
            // Assuming SQLQueryExecutor has a method to execute queries
            _queryExecutor.ExecuteQuery(viewQuery, parameters);
            Console.WriteLine("View operation executed.");  // Log message or further processing
        }

        // If you need to expose logs or results to callers in a more structured way,
        // consider returning a result object or using events or callbacks.
    }
}

