using System;
using Microsoft.Data.SqlClient;
using SQLTest5.Modules.ViewDel;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The DatabaseService class is responsible for executing database operations
    /// using an implementation of the IDatabaseOperation interface.
    /// </summary>
    public class DatabaseService
    {
        private readonly IDatabaseOperation _operation; // Performs database operations.
        private readonly SQLConnectionManager _connectionManager; // Manages SQL connections.

        /// <summary>
        /// Initializes a new instance of the DatabaseService class.
        /// </summary>
        /// <param name="operation">An implementation of the IDatabaseOperation interface.</param>
        /// <param name="connectionManager">An instance of SQLConnectionManager to manage SQL connections.</param>
        /// <exception cref="ArgumentNullException">Thrown when the operation or connectionManager parameter is null.</exception>
        public DatabaseService(IDatabaseOperation operation, SQLConnectionManager connectionManager)
        {
            // Ensure the operation and connectionManager are not null.
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
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

        /// <summary>
        /// Executes a stored procedure and returns a SqlDataReader.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the stored procedure.</param>
        /// <returns>A SqlDataReader containing the result of the stored procedure.</returns>
        public SqlDataReader ExecuteStoredProcedure(string procedureName, SqlParameter[]? parameters = null)
        {
            // Open a new SQL connection
            using (var connection = _connectionManager.GetConnection())
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Add parameters if provided
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    // Execute the command and return the SqlDataReader
                    return command.ExecuteReader();
                }
            }
        }

    }
}
