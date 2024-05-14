using System;
using SQLTest5.Modules;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The DatabaseConnectionManager class is responsible for providing a singleton instance
    /// of the SQLConnectionManager. This ensures that only one instance of the connection manager
    /// is created and used throughout the application.
    /// </summary>
    public class DatabaseConnectionManager
    {
        // A private static field to hold the single instance of SQLConnectionManager.
        private static SQLConnectionManager? _connectionManager;

        // A private static object used for locking to ensure thread safety.
        private static readonly object _lock = new object();

        /// <summary>
        /// Gets the singleton instance of the SQLConnectionManager.
        /// </summary>
        /// <param name="config">An instance of ConfigManager containing configuration details.</param>
        /// <returns>The singleton instance of SQLConnectionManager.</returns>
        public static SQLConnectionManager GetConnectionManager(ConfigManager config)
        {
            // Check if the connection manager instance is null.
            if (_connectionManager == null)
            {
                // Lock the _lock object to ensure thread safety.
                lock (_lock)
                {
                    // Double-check if the connection manager instance is still null.
                    if (_connectionManager == null)
                    {
                        // Initialize the connection manager with the provided configuration.
                        _connectionManager = new SQLConnectionManager(config);
                    }
                }
            }

            // Return the singleton instance of the connection manager.
            return _connectionManager;
        }
    }
}
