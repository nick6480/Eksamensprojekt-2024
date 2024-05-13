// Provides high-level database operations using a strategy for executing SQL queries.
using Microsoft.Data.SqlClient;
using SQLTest5.Modules;
using SQLTest5.Modules.ViewDel;

namespace SQLTest5.Modules.DBAdgang
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
