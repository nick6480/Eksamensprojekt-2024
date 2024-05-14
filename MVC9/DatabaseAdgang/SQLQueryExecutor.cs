using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MVC2.Modules.DatabaseAdgang;

namespace MVC2.Modules.DatabaseAdgang
{
    // Executes SQL queries using an established SQL connection.
    public class SQLQueryExecutor
    {
        private readonly SQLConnectionManager _connectionManager;

        public SQLQueryExecutor(SQLConnectionManager connectionManager)
        {
            _connectionManager = connectionManager ?? throw new ArgumentNullException(nameof(connectionManager));
        }

        // Executes a SQL query and prints the results.
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


        // Overload for executing commands without any parameters
        public void ExecuteQueryAndPrintResults(string sql)
        {
            Console.WriteLine("SQLQ Executing SQL query:");
            ExecuteQueryAndPrintResults(sql, null);

        }

        // Executes a stored procedure.
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



        // Executes a SQL stored function that returns a value.
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

        // Creates and returns a SQL command object configured with the provided SQL query, connection, and parameters.

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

    }
}