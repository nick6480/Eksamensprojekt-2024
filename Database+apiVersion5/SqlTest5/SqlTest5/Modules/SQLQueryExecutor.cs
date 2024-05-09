using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules
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



        // Overload for executing commands without any parameters
        public void ExecuteQueryAndPrintResults(string sql)
        {
            ExecuteQueryAndPrintResults(sql, null);
        }

        // Executes a stored procedure.
        public void ExecuteStoredProcedure(string procedureName, SqlParameter[]? parameters=null)
        {
            string sql = $"EXEC {procedureName}";
            ExecuteQueryAndPrintResults(sql, parameters);
        }

        // Executes a SQL function that returns a value.
        public object ExecuteScalarFunction(string functionName, SqlParameter[] parameters)
        {
            string paramList = parameters != null ? string.Join(", ", parameters.Select(p => $"@{p.ParameterName}")) : "";
            string sql = $"SELECT {functionName}({paramList})";
            using (SqlConnection connection = _connectionManager.GetConnection())
            using (SqlCommand command = CreateCommand(sql, connection, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        // Creates and returns a SQL command object configured with the provided SQL query, connection, and parameters.
        private SqlCommand CreateCommand(string sql, SqlConnection connection, SqlParameter[]? parameters = null)
        {
            SqlCommand command = new SqlCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }
    }
}
