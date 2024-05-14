using System;
using MVC2.Modules.DatabaseAdgang;
using Microsoft.Data.SqlClient;

namespace MVC2.Modules
{
    public class ViewOperation : IDatabaseOperation
    {
        private readonly SQLQueryExecutor _queryExecutor;

        public ViewOperation(SQLQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public void Execute(string viewQuery, SqlParameter[]? parameters = null)
        {
            _queryExecutor.ExecuteQueryAndPrintResults(viewQuery, parameters);
            Console.WriteLine("ViewOperation"); // Indicates execution of view operation.
        }
        public static string FetchViewData(string query)
        {
            return "Data based on " + query;
        }
    }
}

