using System;
using Microsoft.Data.SqlClient;
using SQLTest5.Modules;

namespace SQLTest5.Modules.StoredMethods
{
    public class StoredProcedureExecutor : IStoredProcedureExecutor
    {
        private readonly SQLQueryExecutor _sqlExecutor;

        public StoredProcedureExecutor(SQLQueryExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            _sqlExecutor.ExecuteStoredProcedure(procedureName, parameters);
        }
    }
}

