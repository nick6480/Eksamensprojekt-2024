using System;
using Microsoft.Data.SqlClient;
using System.Data;
//using Microsoft.Extensions.DependencyInjection;


using System.Data.SqlClient;

using System;

using SQLTest5.Modules.DBAdgang;
using SQLTest5.Modules.StoredMethods;
using SQLTest5.Modules.ViewDel;

//class Program
//{
//    static void Main(string[] args)
//    {
//        // Create an instance of the DatabaseInitializer.
//        DatabaseInitializer initializer = new DatabaseInitializer();

//        // Initialize the database service using the initializer.
//        var sqlExecutor = initializer.InitializeDatabaseService();

//        // Create instances for function and procedure executors using the initialized SQLQueryExecutor.
//        StoredFunctionExecutor functionExecutor = new StoredFunctionExecutor(sqlExecutor);
//        StoredProcedureExecutor procedureExecutor = new StoredProcedureExecutor(sqlExecutor);

//        try
//        {
//            // Example of executing a stored function
//            Console.WriteLine("Executing stored function 'GetTotalCount'...");
//            object totalCount = functionExecutor.ExecuteScalarFunction("GetTotalCount", new SqlParameter[] { });
//            Console.WriteLine($"Total Count: {totalCount}");

//            // Example of executing a stored procedure
//            Console.WriteLine("Executing stored procedure 'UpdateCustomerStatus'...");
//            procedureExecutor.ExecuteStoredProcedure("UpdateCustomerStatus", new SqlParameter[]
//            {
//                new SqlParameter("@CustomerId", 123),
//                new SqlParameter("@Status", "Active")
//            });
//            Console.WriteLine("Customer status updated successfully.");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"An error occurred: {ex.Message}");
//        }

//        Console.WriteLine("Press any key to exit...");
//        Console.ReadKey();
//    }
//}





class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a DatabaseInitializer instance.
            var initializer = new DatabaseInitializer();

            // Initialize the database service using the initializer.
            var databaseService = initializer.InitializeDatabaseService();

            // Define parameters for the stored function
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Param1", SqlDbType.Int) { Value = 1 },
                // Add more parameters as needed
            };

            // Assume the function name is 'CalculateSomething' and it expects parameters
            object result = SQLQueryExecutor.ExecuteScalarFunction("CalculateSomething", parameters);
            Console.WriteLine("Result of the stored function: " + result);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
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
//            // Create a DatabaseInitializer instance.
//            var initializer = new DatabaseInitializer();

//            // Initialize the database service using the initializer.
//            var databaseService = initializer.InitializeDatabaseService();

//            // Example usage of the database service to perform view operations.
//            Console.WriteLine("Executing operation on 'PersonView':");
//            databaseService.ExecuteOperation("SELECT * FROM [Test22].[dbo].[GetSortNames]");
//            Console.WriteLine("View - Program");
//            databaseService.ExecuteOperation("SELECT * FROM [Test22].[dbo].[StudentEmails]");


//            //// Example usage of the stored function to retrieve credit information.
//            //var creditInfo = StoredFunctionExecutor.GetCreditInformation(databaseService);
//            //Console.WriteLine($"Credit Information: {creditInfo}");
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

////VIEW//
//class Program
//{
//    static void Main(string[] args)
//    {
//        try
//        {
//            var configPath = "config.json"; // Path to the configuration file.
//            var config = ConfigManager.LoadConfig(configPath); // Load configuration settings.
//            var connectionManager = new SQLConnectionManager(config); // Initialize SQL connection manager.
//            var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.

//            // Initialize ViewOperation class for performing view operations.
//            var viewOperation = new ViewOperation(queryExecutor);
//            var databaseService = new DatabaseService(viewOperation); // Initialize database service.

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
