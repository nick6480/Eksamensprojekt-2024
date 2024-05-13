using System;
using Microsoft.Data.SqlClient;

namespace MVC.Modules.APIService
{
    public interface IDatabaseOperation
    {
        // Execute database operations.
        void Execute(string query, SqlParameter[]? parameters = null);
        //object ExecuteScalar(string sqlCommand);

    }
}

