using System;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules.StoredMethods
{
    public interface IStoredProcedureExecutor
    {
        void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters);
    }
}

