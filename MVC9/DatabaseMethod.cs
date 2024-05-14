using System;
using System.Collections.Generic;
using static MVC2.Modules.DatabaseMethod;

namespace MVC2.Modules
{
    
        public class DatabaseMethod : IDatabaseMethod
        {
            private List<StoredProcedure> storedProcedures = new List<StoredProcedure>();
            private List<StoredFunction> storedFunctions = new List<StoredFunction>();
            private List<DatabaseView> databaseViews = new List<DatabaseView>();

            public void AddStoredProcedure(StoredProcedure storedProcedure)
            {
                storedProcedures.Add(storedProcedure);
            }

            public void AddStoredFunction(StoredFunction storedFunction)
            {
                storedFunctions.Add(storedFunction);
            }

            public void AddDatabaseView(DatabaseView databaseView)
            {
                databaseViews.Add(databaseView);
            }

            public List<StoredProcedure> GetStoredProcedures()
            {
                return storedProcedures;
            }

            public List<StoredFunction> GetStoredFunctions()
            {
                return storedFunctions;
            }

            public List<DatabaseView> GetDatabaseViews()
            {
                return databaseViews;
            }
        }

    
}

