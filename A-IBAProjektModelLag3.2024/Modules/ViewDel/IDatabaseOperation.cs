using System;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules.ViewDel
{
    /// <summary>
    /// The IDatabaseOperation interface defines a contract for database operations.
    /// Any class implementing this interface must provide an implementation for the
    /// Execute method.
    /// </summary>
    public interface IDatabaseOperation
    {
        /// <summary>
        /// Executes a database operation using the provided query and parameters.
        /// </summary>
        /// <param name="query">The SQL query string to execute.</param>
        /// <param name="parameters">
        /// An optional array of SqlParameter objects for the query. These parameters
        /// allow for the safe insertion of values into the query.
        /// </param>
        void Execute(string query, SqlParameter[]? parameters = null);

        // Note: The following method is commented out, but if needed, it could be
        // included to allow for scalar value retrieval from the database.
        // /// <summary>
        // /// Executes a database command and returns a single scalar value.
        // /// </summary>
        // /// <param name="sqlCommand">The SQL command string to execute.</param>
        // /// <returns>The result of the command execution as an object.</returns>
        // object ExecuteScalar(string sqlCommand);
    }
}

