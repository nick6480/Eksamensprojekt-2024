using System;
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
        public void ExecuteQueryAndPrintResults(string sql)
        {
            using (SqlConnection connection = _connectionManager.GetConnection())
            using (SqlCommand command = CreateCommand(sql, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                // Read and print each row from the result set.
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2}", reader[0], reader[1], reader[2]);
                }
            }
        }

        // Creates and returns a SQL command object configured with the provided SQL query and connection.
        private SqlCommand CreateCommand(string sql, SqlConnection connection)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query cannot be null or empty.", nameof(sql));
            return new SqlCommand(sql, connection);
        }
    }
}
