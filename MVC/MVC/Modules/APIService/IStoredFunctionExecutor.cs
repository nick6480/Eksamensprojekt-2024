using System;
using Microsoft.Data.SqlClient;
namespace MVC.Modules.APIService
{
    public interface IStoredFunctionExecutor
    {
        object ExecuteScalarFunction(string functionName, SqlParameter[] parameters);
    }
}

