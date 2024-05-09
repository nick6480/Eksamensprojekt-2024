using System;
using Microsoft.Data.SqlClient;
using SQLTest5.Modules;

namespace SqlTest5.Modules
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
        }
    }

}

