using System;
using SQLTest5.Modules.DBAdgang;

namespace SqlTest5.Modules.Autentication
{
    public class LoginService
    {
        private readonly SQLQueryExecutor _queryExecutor;

        public LoginService(SQLQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        // Method to login the user.
        public bool Login(string username, string password)
        {
            return _queryExecutor.VerifyUser(username, password);
        }
    }
}