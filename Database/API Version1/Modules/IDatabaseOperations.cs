using System;
using Microsoft.Data.SqlClient;

namespace TestAfAlt.Modules
{
    // Interface for database operations
    public interface IDatabaseOperations
    {
        void GetDataFromTable(string tableName);
        void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters);
        object ExecuteScalarFunction(string functionName, SqlParameter[] parameters);
    }

}

