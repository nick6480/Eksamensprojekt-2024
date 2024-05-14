using System;
using Microsoft.Data.SqlClient;

namespace MVC2.Modules.DatabaseAdgang
{
    // Manages SQL connections using provided configuration details.
    public class SQLConnectionManager
    {
        private readonly SqlConnectionStringBuilder _builder;

        public SQLConnectionManager(ConfigManager config)
        {
            ValidateConfig(config);  // Validate configuration before using it.
            // Configure the SQL connection string with required parameters.
            _builder = new SqlConnectionStringBuilder
            {
                DataSource = config.DataSource,
                UserID = config.UserId,
                Password = config.Password,
                InitialCatalog = config.InitialCatalog,
                TrustServerCertificate = true  // Optionally trust the server certificate.
            };
        }

        // Validates the necessary fields of the configuration.
        private void ValidateConfig(ConfigManager config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Configuration data is null.");
            if (string.IsNullOrEmpty(config.DataSource) || string.IsNullOrEmpty(config.UserId) ||
                string.IsNullOrEmpty(config.Password) || string.IsNullOrEmpty(config.InitialCatalog))
            {
                throw new ArgumentException("Configuration properties cannot be null or empty.", nameof(config));
            }
        }

        // Opens and returns a new SQL connection.
        public SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_builder.ConnectionString);
                connection.Open();  // Attempt to open the connection.
                return connection;
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine($"SQL connection failed: {ex.Message}");
                throw new InvalidOperationException("Failed to open SQL connection.", ex);
            }
        }
    }
}
