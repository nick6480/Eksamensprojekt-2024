using System;
using System.Collections.Generic;
namespace MVC2.Modules
{
	public interface IDatabaseMethod
    {
        void AddStoredProcedure(StoredProcedure storedProcedure);
        void AddStoredFunction(StoredFunction storedFunction);
        void AddDatabaseView(DatabaseView databaseView);
        List<StoredProcedure> GetStoredProcedures();
        List<StoredFunction> GetStoredFunctions();
        List<DatabaseView> GetDatabaseViews();
    }
}

