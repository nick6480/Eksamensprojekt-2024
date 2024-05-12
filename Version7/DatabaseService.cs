// Provides high-level database operations using a strategy for executing SQL queries.
using Microsoft.Data.SqlClient;
using SqlTest5.Modules;

namespace SQLTest5.Modules
{
    public class DatabaseService
    {
        private readonly IDatabaseOperation _operation; // Performs database operations.

        public DatabaseService(IDatabaseOperation operation)
        {
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        // Executes a specified SQL operation.
        public void ExecuteOperation(string commandText, SqlParameter[]? parameters = null)
        {
            _operation.Execute(commandText, parameters);
        }
    }
}
