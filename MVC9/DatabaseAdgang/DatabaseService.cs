// Provides high-level database operations using a strategy for executing SQL queries.
using Microsoft.Data.SqlClient;
using MVC2.Modules;


namespace MVC2.Modules.DatabaseAdgang
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
        // Simulated method to execute a SQL command or stored procedure.
        public void ExecuteSqlCommand(string sqlCommand)
        {
            Console.WriteLine($"Executing SQL command: {sqlCommand}");
        }
    }
}
