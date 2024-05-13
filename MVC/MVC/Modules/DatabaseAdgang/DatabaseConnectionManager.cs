using System;
using MVC.Modules;

namespace MVC.Modules.DatabaseAdgang
{
    public class DatabaseConnectionManager
    {
        private static SQLConnectionManager? _connectionManager;
        private static readonly object _lock = new object();

        // Get the SQL connection manager instance.
        public static SQLConnectionManager GetConnectionManager(ConfigManager config)
        {
            if (_connectionManager == null)
            {
                lock (_lock)
                {
                    if (_connectionManager == null)
                    {
                        _connectionManager = new SQLConnectionManager(config);
                    }
                }
            }

            return _connectionManager;
        }
    }
}

