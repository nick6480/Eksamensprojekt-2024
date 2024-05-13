using System;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules.ViewDel
{
    public interface IDatabaseOperation
    {
        // Execute database operations.
        void Execute(string query, SqlParameter[]? parameters = null);
        //object ExecuteScalar(string sqlCommand);

    }
}

