using System;
using System.Collections.Generic;
using TestAfAlt.Modules;



namespace TestAfAlt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the path to the configuration file including the filename
            string configPath = "/Users/allanfrank/Projects/TestAfAlt/TestAfAlt/bin/Debug/net7.0/config.json";

            try
            {
                // Load the configuration
                ConfigManager config = ConfigManager.LoadConfig(configPath);

                // Create SQLConnectionManager with the loaded configuration
                SQLConnectionManager connectionManager = new SQLConnectionManager(config);

                // Create SQLQueryExecutor with the connection manager
                SQLQueryExecutor queryExecutor = new SQLQueryExecutor(connectionManager);

                // Create DatabaseService and run a test query
                DatabaseService dbService = new DatabaseService(queryExecutor);
                dbService.GetDataFromTable("Person");  // Replace 'Person' with an actual table name if different

                // Handling of database objects like views, stored procedures, and functions
                DatabaseAPI databaseAP = new DatabaseAPI(configPath);
                databaseAP.AddDatabaseView(new DatabaseView("TestView", "SELECT * FROM Person"));
                databaseAP.AddStoredProcedure(new StoredProcedure("TestProcedure", new string[] { "param1", "param2" }, "void"));
                databaseAP.AddStoredFunction(new StoredFunction("TestFunction", new string[] { "param1" }, "int"));

                // Print stored procedures
                var procedures = databaseAP.GetStoredProcedures();
                foreach (var proc in procedures)
                {
                    Console.WriteLine($"Procedure: {proc.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}








//class Program
//{
//    static void Main(string[] args)
//    {
//        // Create an instance of the middleware
//        DatabaseAP middleware = new DatabaseAP();

//        // Create and add stored procedures
//        StoredProcedure sp1 = new StoredProcedure("GetUserById", new string[] { "userId" }, "User");
//        StoredProcedure sp2 = new StoredProcedure("DeleteUser", new string[] { "userId" }, "void");
//        middleware.AddStoredProcedure(sp1);
//        middleware.AddStoredProcedure(sp2);

//        // Create and add stored functions
//        StoredFunction sf1 = new StoredFunction("CalculateAge", new string[] { "birthDate" }, "int");
//        StoredFunction sf2 = new StoredFunction("UserExists", new string[] { "email" }, "bool");
//        middleware.AddStoredFunction(sf1);
//        middleware.AddStoredFunction(sf2);

//        // Create and add database views
//        DatabaseView dv1 = new DatabaseView("UserView", "SELECT * FROM Users");
//        DatabaseView dv2 = new DatabaseView("ProductView", "SELECT * FROM Products");
//        middleware.AddDatabaseView(dv1);
//        middleware.AddDatabaseView(dv2);

//        // Display stored procedures
//        Console.WriteLine("Stored Procedures:");
//        foreach (var sp in middleware.GetStoredProcedures())
//        {
//            Console.WriteLine($"Name: {sp.Name}, Return Type: {sp.ReturnType}, Parameters: {string.Join(", ", sp.Parameters)}");
//        }

//        // Display stored functions
//        Console.WriteLine("\nStored Functions:");
//        foreach (var sf in middleware.GetStoredFunctions())
//        {
//            Console.WriteLine($"Name: {sf.Name}, Return Type: {sf.ReturnType}, Parameters: {string.Join(", ", sf.Parameters)}");
//        }

//        // Display database views
//        Console.WriteLine("\nDatabase Views:");
//        foreach (var dv in middleware.GetDatabaseViews())
//        {
//            Console.WriteLine($"Name: {dv.Name}, Query: {dv.Query}");
//        }
//    }
//}
