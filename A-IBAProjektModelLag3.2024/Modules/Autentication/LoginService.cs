using System;
using NLog;
using SQLTest5.Modules.DBAdgang;

namespace SqlTest5.Modules.Autentication
{
    public class LoginService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly SQLQueryExecutor _queryExecutor;


        public LoginService(SQLQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        // Method to login the user.
        public bool Login(string username, string password)
        {
            Logger.Info($"Attempting login for user '{username}'");
            bool success = _queryExecutor.VerifyUser(username, password);
            Logger.Info(success ? $"Login successful for user '{username}'" : $"Login failed for user '{username}'");
            return success;
        }
    }
}

