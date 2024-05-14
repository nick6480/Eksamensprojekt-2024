using System;
using SQLTest5.Modules.DBAdgang;
using Microsoft.Data.SqlClient;

namespace SQLTest5.Modules.ViewDel
{
    /// <summary>
    /// The ViewOperation class implements the IDatabaseOperation interface to handle
    /// database view operations. It utilizes an SQLQueryExecutor to execute the queries.
    /// </summary>
    public class ViewOperation : IDatabaseOperation
    {
        // An instance of SQLQueryExecutor to perform query executions.
        private readonly SQLQueryExecutor _queryExecutor;

        /// <summary>
        /// Initializes a new instance of the ViewOperation class.
        /// </summary>
        /// <param name="queryExecutor">An instance of SQLQueryExecutor used for executing queries.</param>
        /// <exception cref="ArgumentNullException">Thrown when the queryExecutor parameter is null.</exception>
        public ViewOperation(SQLQueryExecutor queryExecutor)
        {
            // Check if the queryExecutor is null and throw an exception if it is.
            _queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        /// <summary>
        /// Executes a view query and prints the results.
        /// </summary>
        /// <param name="viewQuery">The SQL query string to execute.</param>
        /// <param name="parameters">An optional array of SqlParameter objects for the query.</param>
        public void Execute(string viewQuery, SqlParameter[]? parameters = null)
        {
            // Execute the query using the provided SQLQueryExecutor instance.
            _queryExecutor.ExecuteQueryAndPrintResults(viewQuery, parameters);

            // Print a message indicating that the view operation has been executed.
            Console.WriteLine("ViewOperation");
        }
    }
}