// Implements the IDatabaseOperation interface for performing view operations.



using System;
using SQLTest5.Modules.StoredMethods;
using SQLTest5.Modules.DBAdgang;
using Microsoft.Data.SqlClient;
//using SQLTest5.Modules;

namespace SQLTest5.Modules.ViewDel
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
    }
}








//using Microsoft.Data.SqlClient;
//using SQLTest5.Modules;

//namespace SqlTest5.Modules
//{
//    public class ViewOperation : IDatabaseOperation
//    {
//        private readonly SQLQueryExecutor _queryExecutor; // Responsible for executing SQL queries.

//        public ViewOperation(SQLQueryExecutor queryExecutor)
//        {
//            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
//        }

//        // Executes a view query and prints the results.
//        public void Execute(string viewQuery, SqlParameter[]? parameters = null)
//        {
//            _queryExecutor.ExecuteQueryAndPrintResults(viewQuery, parameters);
//            Console.WriteLine("ViewOperation"); // Indicates execution of view operation.
//        }
//    }
//}
