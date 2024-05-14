using System;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The SQLConnectionManager class is responsible for managing SQL connections
    /// using provided configuration details.
    /// </summary>
    public class SQLConnectionManager
    {
        // A private field to hold the SQL connection string builder instance.
        private readonly SqlConnectionStringBuilder _builder;

        /// <summary>
        /// Initializes a new instance of the SQLConnectionManager class.
        /// </summary>
        /// <param name="config">An instance of ConfigManager containing configuration details.</param>
        /// <exception cref="ArgumentNullException">Thrown when the config parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when any configuration property is null or empty.</exception>
        public SQLConnectionManager(ConfigManager config)
        {
            // Validate the configuration before using it.
            ValidateConfig(config);

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

        /// <summary>
        /// Validates the necessary fields of the configuration.
        /// </summary>
        /// <param name="config">An instance of ConfigManager containing configuration details.</param>
        /// <exception cref="ArgumentNullException">Thrown when the config parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown when any configuration property is null or empty.</exception>
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

        /// <summary>
        /// Opens and returns a new SQL connection.
        /// </summary>
        /// <returns>An open SqlConnection instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the SQL connection fails to open.</exception>
        public SqlConnection GetConnection()
        {
            try
            {
                // Create a new SQL connection using the connection string.
                var connection = new SqlConnection(_builder.ConnectionString);
                connection.Open();  // Attempt to open the connection.
                return connection;  // Return the open connection.
            }
            catch (SqlException ex)
            {
                // Log the error and throw an exception to be handled by the caller.
                Console.Error.WriteLine($"SQL connection failed: {ex.Message}");
                throw new InvalidOperationException("Failed to open SQL connection.", ex);
            }
        }
    }
}