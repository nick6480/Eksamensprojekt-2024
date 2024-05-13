using System;
namespace MVC.Modules.APIService
{
    public interface IDatabaseService
    {
        void AddStoredProcedure(StoredProcedure storedProcedure);
        void AddStoredFunction(StoredFunction storedFunction);
        void AddDatabaseView(DatabaseView databaseView);
        List<StoredProcedure> GetStoredProcedures();
        List<StoredFunction> GetStoredFunctions();
        List<DatabaseView> GetDatabaseViews();
    }
}

