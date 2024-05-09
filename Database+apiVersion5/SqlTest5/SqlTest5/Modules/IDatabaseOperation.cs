using System;
using Microsoft.Data.SqlClient;

namespace SqlTest5.Modules
{
    public interface IDatabaseOperation
    {
        void Execute(string query, SqlParameter[]? parameters = null);
    }
}

