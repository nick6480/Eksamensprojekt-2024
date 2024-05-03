using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQLTest2
{
    public class SQLQueryExecutor
    {
        // Holds a reference to SQLConnectionManager.
        private SQLConnectionManager _connectionManager;

        public SQLQueryExecutor(SQLConnectionManager connectionManager)
        {
            // Initializes the connection manager in the constructor.
            _connectionManager = connectionManager;
        }

        public void ExecuteQueryAndPrintResults(string sql)
        {
            // Uses the GetConnection method to open a database connection.
            using (SqlConnection connection = _connectionManager.GetConnection())
            {
                // Creates a SqlCommand object to perform the SQL query.
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Executes the SQL command and obtains a SqlDataReader to read the results.
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())  // Reads each row in the result set.
                        {
                            // Prints the values from each column in the current row.
                            Console.WriteLine("{0} {1} {2}", reader[0], reader[1], reader[2]);
                        }
                    }
                }
            }
        }
    }
}

