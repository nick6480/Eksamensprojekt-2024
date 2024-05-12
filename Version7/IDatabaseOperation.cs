using System;
using Microsoft.Data.SqlClient;

namespace SqlTest5.Modules
{
    public interface IDatabaseOperation
    {
        // Execute database operations.
        void Execute(string query, SqlParameter[]? parameters = null);
        
    }
}

