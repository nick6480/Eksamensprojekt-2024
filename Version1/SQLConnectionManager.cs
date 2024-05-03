using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SQLTest2
{
    public class SQLConnectionManager
    {
        // Private field to hold the SQL connection string builder.
        private SqlConnectionStringBuilder _builder;

        public SQLConnectionManager(string dataSource, string userId, string password, string initialCatalog)
        {
            _builder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,  // Specifies the name of the data source (server name or IP address).
                UserID = userId, // User ID for database authentication.
                Password = password,   // Password for database authentication.
                InitialCatalog = initialCatalog, // Name of the database.
                TrustServerCertificate = true  // Allows the use of unverified SSL certificates.
            };
        }

        public SqlConnection GetConnection()
        {
            try
            {
                // Creates a new connection based on the connection string.
                SqlConnection connection = new SqlConnection(_builder.ConnectionString);
                connection.Open();  // Opens the connection.
                return connection;  // Returns the opened connection.
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());// Prints errors if an SQL Exception occurs.
                throw; // Rethrows the exception.
            }
        }
    }

}

