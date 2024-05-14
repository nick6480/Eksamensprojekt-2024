using System;
using System.Data;
using Microsoft.Data.SqlClient;
using NLog;
using SQLTest5.Modules.DBAdgang;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The SQLQueryExecutor class is responsible for executing SQL queries and stored procedures
    /// using a provided SQL connection manager.
    /// </summary>
    public class SQLQueryExecutor
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        // A private field to hold the connection manager instance.
        private readonly SQLConnectionManager _connectionManager;

        /// <summary>
        /// Initializes a new instance of the SQLQueryExecutor class.
        /// </summary>
        /// <param name="connectionManager">The connection manager used to obtain SQL connections.</param>
        /// <exception cref="ArgumentNullException">Thrown when the connection manager is null.</exception>
        public SQLQueryExecutor(SQLConnectionManager connectionManager)
        {
            // Ensure the connection manager is not null.
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        }

        /// <summary>
        /// Executes a SQL query and prints the results to the console.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the query.</param>
        /// <exception cref="ArgumentException">Thrown when the SQL query is null or empty.</exception>
        public void ExecuteQueryAndPrintResults(string sql, SqlParameter[]? parameters = null)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentException("SQL command cannot be null or empty.", nameof(sql));

            using (SqlConnection connection = _connectionManager.GetConnection())
            using (SqlCommand command = CreateCommand(sql, connection, parameters))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                    }
                    Console.WriteLine(); // Adds a newline for better readability
                }
            }
        }

        /// <summary>
        /// Executes a SQL query and returns the results as a DataTable.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the query.</param>
        /// <returns>A DataTable containing the results of the query.</returns>
        public DataTable ExecuteQuery(string sql, SqlParameter[]? parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = _connectionManager.GetConnection())
            using (SqlCommand command = CreateCommand(sql, connection, parameters))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
            }
            return dataTable;
        }

        /// <summary>
        /// Overload of ExecuteQueryAndPrintResults that executes a SQL query without any parameters.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        public void ExecuteQueryAndPrintResults(string sql)
        {
            Console.WriteLine("SQLQ Executing SQL query:");
            ExecuteQueryAndPrintResults(sql, null);
        }

        /// <summary>
        /// Executes a stored procedure.
        /// </summary>
        /// <param name="procedureName">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the stored procedure.</param>
        public void ExecuteStoredProcedure(string procedureName, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                using (SqlCommand command = CreateCommand(procedureName, connection, parameters))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Consider logging the exception or wrapping and re-throwing
                Console.WriteLine("SQL Error: " + ex.Message);
                throw; // Re-throwing to allow further handling up the stack
            }
        }

        /// <summary>
        /// Executes a SQL stored function that returns a value.
        /// </summary>
        /// <param name="functionName">The name of the stored function to execute.</param>
        /// <param name="parameters">An array of SqlParameter objects for the function.</param>
        /// <returns>The result of the function execution as an object.</returns>
        public object ExecuteScalarFunction(string functionName, SqlParameter[] parameters)
        {
            string paramList = parameters != null ? string.Join(", ", parameters.Select(p => $"@{p.ParameterName}")) : "";
            string sql = $"SELECT {functionName}({paramList})";
            using (SqlConnection connection = _connectionManager.GetConnection())
            using (SqlCommand command = CreateCommand(sql, connection, parameters))
            {
                Console.WriteLine($"SQLQ Executing scalar function '{functionName}':");
                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Creates and returns a SQL command object configured with the provided SQL query, connection, and parameters.
        /// </summary>
        /// <param name="sql">The SQL query string to execute.</param>
        /// <param name="connection">The SQL connection to use for the command.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the command.</param>
        /// <returns>A configured SqlCommand object.</returns>
        private SqlCommand CreateCommand(string sql, SqlConnection connection, SqlParameter[]? parameters = null)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    // Debug output to check parameter details
                    Console.WriteLine($"Adding parameter: {param.ParameterName} with value {param.Value ?? "NULL"}");

                    if (param.Value == null)
                        param.Value = DBNull.Value; // Convert null to DBNull.Value
                    command.Parameters.Add(param);
                }
            }
            return command;
        }


        // Method to verify if a user exists in the database.
        public bool VerifyUser(string username, string password)
        {
            Logger.Info($"Verifying user '{username}'");
            string sql = "SELECT COUNT(1) FROM Login WHERE Brugernavn = @Username AND Password = @Password";
            //string sql = $"SELECT COUNT(1) FROM {Login} WHERE Brugernavn = @Username AND Password = @Password";
            try
            {
                using (SqlConnection connection = _connectionManager.GetConnection())
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar) { Value = username });
                    command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = password });

                    int count = (int)command.ExecuteScalar();
                    bool isValidUser = count > 0;
                    Logger.Info(isValidUser ? $"User '{username}' verified successfully" : $"User '{username}' verification failed");
                    return isValidUser;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error verifying user '{username}'");
                throw;
            }
        }
    }
    //// Method to verify if a user exists in the database.
    //public bool VerifyUser(string username, string password)
    //{
    //    // SQL query to check if the user exists with the provided username and password.
    //    Logger.Info($"Verifying user '{username}'");
    //    string sql = "SELECT COUNT(1) FROM Login WHERE Brugernavn = @Username AND Password = @Password";
    //    using (SqlConnection connection = _connectionManager.GetConnection())
    //    using (SqlCommand command = new SqlCommand(sql, connection))
    //    {
    //        // Add parameters to prevent SQL injection.
    //        command.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar) { Value = username });
    //        command.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar) { Value = password });

    //        // Execute the query and get the result.
    //        int count = (int)command.ExecuteScalar();
    //        return count > 0;
    //    }
    //}
}

