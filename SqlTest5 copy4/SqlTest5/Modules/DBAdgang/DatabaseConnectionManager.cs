using System;
using SQLTest5.Modules;

namespace SQLTest5.Modules.DBAdgang
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

