using System;
using System;
using Microsoft.Data.SqlClient;
using SqlTest5.Modules;

    namespace SQLTest5.Modules
    {
        // Provides high-level database operations using a strategy for executing SQL queries.
        public class DatabaseService
        {
            private readonly IDatabaseOperation _operation;

            public DatabaseService(IDatabaseOperation operation)
            {
                _operation = operation ?? throw new ArgumentNullException(nameof(operation));
            }

            // Executes a specified SQL operation.
            public void ExecuteOperation(string commandText, SqlParameter[]? parameters = null)
            {
                _operation.Execute(commandText, parameters);
            }
        }
    }

  