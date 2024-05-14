using Microsoft.Data.SqlClient;
using SQLTest5.Modules;
using SQLTest5.Modules.ViewDel;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The DatabaseService class is responsible for executing database operations
    /// using an implementation of the IDatabaseOperation interface.
    /// </summary>
    public class DatabaseService
    {
        // A private field that holds the database operation instance.
        private readonly IDatabaseOperation _operation; // Performs database operations.

        /// <summary>
        /// Initializes a new instance of the DatabaseService class.
        /// </summary>
        /// <param name="operation">An implementation of the IDatabaseOperation interface.</param>
        /// <exception cref="ArgumentNullException">Thrown when the operation parameter is null.</exception>
        public DatabaseService(IDatabaseOperation operation)
        {
            // Ensure the operation is not null.
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        /// <summary>
        /// Executes a specified SQL operation.
        /// </summary>
        /// <param name="commandText">The SQL command or query to execute.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the command.</param>
        public void ExecuteOperation(string commandText, SqlParameter[]? parameters = null)
        {
            // Execute the command using the provided operation instance.
            _operation.Execute(commandText, parameters);
        }

        /// <summary>
        /// Simulated method to execute a SQL command or stored procedure.
        /// </summary>
        /// <param name="sqlCommand">The SQL command or stored procedure to execute.</param>
        public void ExecuteSqlCommand(string sqlCommand)
        {
            // Simulate executing the SQL command.
            Console.WriteLine($"Executing SQL command: {sqlCommand}");
        }
    }
}
