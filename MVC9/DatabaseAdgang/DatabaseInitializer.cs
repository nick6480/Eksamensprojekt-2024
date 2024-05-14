using System;
using MVC2.Modules;


namespace MVC2.Modules.DatabaseAdgang
{

    public class DatabaseInitializer
    {
        private readonly string _configPath;

        public DatabaseInitializer(string configPath)
        {
            _configPath = configPath ?? throw new ArgumentNullException(nameof(configPath));
        }

        // Initialize the database service and related components.
        public DatabaseService InitializeDatabaseService()
        {
            var config = ConfigManager.LoadConfig(_configPath); // Load configuration settings.
            var connectionManager = new SQLConnectionManager(config); // Initialize SQL connection manager.
            var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.

            // Initialize ViewOperation class for performing view operations.
            var viewOperation = new ViewOperation(queryExecutor);
            return new DatabaseService(viewOperation); // Initialize and return the database service.
        }
    }



    //public class DatabaseInitializer
    //{
    //    // Initialize the database service using the configuration.
    //    public DatabaseService InitializeDatabaseService()
    //    {
    //        var configPath = "config.json"; // Set the path to the configuration file.
    //        var config = ConfigManager.LoadConfig(configPath); // Load configuration settings.

    //        var connectionManager = DatabaseConnectionManager.GetConnectionManager(config); // Get SQL connection manager instance.
    //        var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.

    //        // Initialize ViewOperation class for performing view operations.
    //        var viewOperation = new ViewOperation(queryExecutor);
    //        return new DatabaseService(viewOperation); // Initialize and return the database service.
    //    }
    //}

    //public class DatabaseInitializer
    //{
    //    private readonly string _configPath;

    //    public DatabaseInitializer(string configPath)
    //    {
    //        _configPath = configPath ?? throw new ArgumentNullException(nameof(configPath));
    //    }

    //    // Initialize the database service and related components.
    //    public DatabaseService InitializeDatabaseService()
    //    {
    //        var config = ConfigManager.LoadConfig(_configPath); // Load configuration settings.
    //        var connectionManager = new SQLConnectionManager(config); // Initialize SQL connection manager.
    //        var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.

    //        // Initialize ViewOperation class for performing view operations.
    //        var viewOperation = new ViewOperation(queryExecutor);
    //        return new DatabaseService(viewOperation); // Initialize and return the database service.
    //    }
    //}
}







//using System;
//using SqlTest5.Modules;


//    namespace SQLTest5.Modules
//    {
//    public class DatabaseInitializer
//    {
//        private static DatabaseService _databaseService;
//        private static readonly object _lock = new object();

//        private readonly string _configPath;

//        public DatabaseInitializer(string configPath)
//        {
//            _configPath = configPath ?? throw new ArgumentNullException(nameof(configPath));
//        }

//        // Initialize the database service and related components.
//        public DatabaseService GetDatabaseService()
//        {
//            // Implementing singleton pattern to ensure only one instance of DatabaseService is created.
//            if (_databaseService == null)
//            {
//                lock (_lock)
//                {
//                    if (_databaseService == null)
//                    {
//                        var config = ConfigManager.LoadConfig(_configPath); // Load configuration settings.
//                        var connectionManager = new SQLConnectionManager(config); // Initialize SQL connection manager.
//                        var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.

//                        // Initialize ViewOperation class for performing view operations.
//                        var viewOperation = new ViewOperation(queryExecutor);
//                        _databaseService = new DatabaseService(viewOperation); // Initialize and store the database service.
//                    }
//                }
//            }

//            return _databaseService;
//        }
//    }
//}




