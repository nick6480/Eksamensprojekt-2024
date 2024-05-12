// Implements the IDatabaseOperation interface for performing view operations.
using Microsoft.Data.SqlClient;
using SQLTest5.Modules;

namespace SqlTest5.Modules
{
    public class ViewOperation : IDatabaseOperation
    {
        private readonly SQLQueryExecutor _queryExecutor; // Responsible for executing SQL queries.

        public ViewOperation(SQLQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        // Executes a view query and prints the results.
        public void Execute(string viewQuery, SqlParameter[]? parameters = null)
        {
            _queryExecutor.ExecuteQueryAndPrintResults(viewQuery, parameters);
            Console.WriteLine("ViewOperation"); // Indicates execution of view operation.
        }
    }
}
