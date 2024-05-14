using System;
using SqlTest5.Modules.Autentication;
using SQLTest5.Modules.ViewDel;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The DatabaseInitializer class is responsible for setting up and initializing
    /// the database service and its related components using configuration settings.
    /// </summary>
    public class DatabaseInitializer
    {
        // A private field to hold the path to the configuration file.
        private readonly string _configPath;

        /// <summary>
        /// Initializes a new instance of the DatabaseInitializer class.
        /// </summary>
        /// <param name="configPath">The path to the configuration file.</param>
        /// <exception cref="ArgumentNullException">Thrown when the configPath parameter is null.</exception>
        public DatabaseInitializer(string configPath)
        {
            // Ensure the configPath is not null.
            _configPath = configPath ?? throw new ArgumentNullException(nameof(configPath));
        }

        /// <summary>
        /// Initializes the database service and related components.
        /// </summary>
        /// <returns>An instance of DatabaseService.</returns>
        public DatabaseService InitializeDatabaseService()
        {
            // Load configuration settings from the specified configuration file.
            var config = ConfigManager.LoadConfig(_configPath);

            // Initialize the SQL connection manager using the loaded configuration.
            var connectionManager = new SQLConnectionManager(config);

            // Initialize the SQL query executor using the connection manager.
            var queryExecutor = new SQLQueryExecutor(connectionManager);

            // Initialize the ViewOperation class for performing view operations using the query executor.
            var viewOperation = new ViewOperation(queryExecutor);

            // Initialize and return the database service using the view operation.
            return new DatabaseService(viewOperation);
        }

        // Initialize the login service.
        public LoginService InitializeLoginService()
        {
            var config = ConfigManager.LoadConfig(_configPath); // Load configuration settings.
            var connectionManager = new SQLConnectionManager(config); // Initialize SQL connection manager.
            var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.
            return new LoginService(queryExecutor); // Initialize and return the login service.
        }
    }
}
