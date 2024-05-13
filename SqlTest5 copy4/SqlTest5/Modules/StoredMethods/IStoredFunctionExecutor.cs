using System;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules.StoredMethods
{
    public interface IStoredFunctionExecutor
    {
        object ExecuteScalarFunction(string functionName, SqlParameter[] parameters);
    }
}

