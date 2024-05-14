using System;
using Microsoft.Data.SqlClient;
using MVC2.Modules;
using MVC2.Modules.DatabaseAdgang;

namespace SimpleTestProgram
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing Database Operations...");

            // Simulate fetching data from a database view
            var viewResult = ViewOperation.FetchViewData("Select * from ExampleView");
            Console.WriteLine($"View Data: {viewResult}");

            // Simulate executing a stored procedure
            var spResult = StoredProcedure.Execute("usp_UpdateData");
            Console.WriteLine($"Stored Procedure Execution Result: {spResult}");

            // Simulate executing a stored function
            var functionResult = StoredFunction.ExecuteFunction("CalculateInterest", new object[] { 1000, 0.05 });
            Console.WriteLine($"Function Result: {functionResult}");

            Console.WriteLine("Test Complete.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        try
    //        {
    //            var initializer = new DatabaseInitializer("config.json");
    //            var databaseService = initializer.InitializeDatabaseService();

    //            // Example usage of the database service to perform view operations.
    //            Console.WriteLine("Executing operation on 'PersonView':");
    //            databaseService.ExecuteOperation("SELECT * FROM [Test22].[dbo].[GetSortNames]");
    //            Console.WriteLine("View - Program");
    //            databaseService.ExecuteOperation("SELECT * FROM [Test22].[dbo].[StudentEmails]");

    //            // This structure allows you to easily switch to other operations like stored procedures or functions.
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.Error.WriteLine($"An error occurred: {ex.Message}");
    //        }
    //        Console.WriteLine("Press any key to exit...");
    //        Console.ReadKey();
    //    }
    //}


    //class Program
    //{
    //    static void Main()
    //    {
    //        try
    //        {
    //            // Initialize the database service
    //            var databaseInitializer = new DatabaseInitializer();
    //            var queryExecutor = databaseInitializer.InitializeDatabaseService();

    //            // Execute the stored function using the SQLQueryExecutor
    //            string functionName = "YourFunctionName";
    //            object result = queryExecutor.ExecuteScalarFunction(functionName, new SqlParameter[0]); // Assuming no parameters

    //            Console.WriteLine("Result: " + result);
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("An error occurred: " + ex.Message);
    //        }
    //    }
    //}
    //{
    //    class Program
    //    {
    //        static void Main(string[] args)
    //        {
    //            Console.WriteLine("Testing Database Operations...");

    //            // Simulate fetching data from a database view
    //            var viewResult = DatabaseView.FetchViewData("Select * from ExampleView");
    //            Console.WriteLine($"View Data: {viewResult}");

    //            // Simulate executing a stored procedure
    //            var spResult = StoredProcedure.Execute("usp_UpdateData");
    //            Console.WriteLine($"Stored Procedure Execution Result: {spResult}");

    //            // Simulate executing a stored function
    //            var functionResult = StoredFunction.ExecuteFunction("CalculateInterest", new object[] { 1000, 0.05 });
    //            Console.WriteLine($"Function Result: {functionResult}");

    //            Console.WriteLine("Test Complete.");
    //        }
    //    }

    //    public static class DatabaseView
    //    {
    //        public static string FetchViewData(string query)
    //        {
    //            return "Data based on " + query;
    //        }
    //    }

    //    public static class StoredProcedure
    //    {
    //        public static string Execute(string procedureName)
    //        {
    //            return $"{procedureName} executed successfully.";
    //        }
    //    }

    //    public static class StoredFunction
    //    {
    //        public static double ExecuteFunction(string functionName, object[] parameters)
    //        {
    //            return (double)parameters[0] * (double)parameters[1];
    //        }
    //    }
    //}
}