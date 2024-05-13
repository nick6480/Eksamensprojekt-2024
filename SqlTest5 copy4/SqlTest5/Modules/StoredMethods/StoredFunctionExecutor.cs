using System;
using Microsoft.Data.SqlClient;
using SQLTest5.Modules;

namespace SQLTest5.Modules.StoredMethods
{
    public class StoredFunctionExecutor : IStoredFunctionExecutor
    {
        private readonly SQLQueryExecutor _sqlExecutor;

        public StoredFunctionExecutor(SQLQueryExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public object ExecuteScalarFunction(string functionName, SqlParameter[] parameters)
        {
            return _sqlExecutor.ExecuteScalarFunction(functionName, parameters);
        }
    }

}

