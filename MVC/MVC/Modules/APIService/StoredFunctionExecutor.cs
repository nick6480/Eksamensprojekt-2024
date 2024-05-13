using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using MVC.Modules.APIService;
using MVC.Modules.DatabaseAdgang;

namespace MVC.DataAccess
{
    public class StoredFunctionExecutor
    {
        private readonly SQLQueryExecutor _sqlExecutor;

        public StoredFunctionExecutor(SQLQueryExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        // Asynchronous method to execute a stored function and return a scalar result
        public async Task<object> ExecuteScalarFunctionAsync(StoredFunction function)
        {
            return await _sqlExecutor.ExecuteScalarFunctionAsync(function.Name, function.Parameters.ToArray());
        }

        // Getter method to retrieve function details such as name and parameters
        public string GetFunctionDetails(StoredFunction function)
        {
            return $"Function Name: {function.Name}, Parameters Count: {function.Parameters.Count}";
        }

        // Optionally, if you need to retrieve function results in a type-safe manner
        public async Task<T> ExecuteScalarFunctionAsync<T>(StoredFunction function)
        {
            object result = await _sqlExecutor.ExecuteScalarFunctionAsync(function.Name, function.Parameters.ToArray());
            return (T)Convert.ChangeType(result, typeof(T));

        }
    }
}