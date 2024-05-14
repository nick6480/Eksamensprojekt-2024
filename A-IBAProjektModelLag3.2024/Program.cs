﻿using System;
using NLog;
using SQLTest5.Modules.DBAdgang;
using SqlTest5.Modules.Autentication;

class Program
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {
        try
        {
            Logger.Info("Application started");
            var initializer = new DatabaseInitializer("config.json");

            //Initialize the database service.
            var databaseService = initializer.InitializeDatabaseService();
            Console.WriteLine("Executing operation on 'PersonView':");
            databaseService.ExecuteOperation("SELECT * FROM [IBA2024].[dbo].[AllStudentEmails]");
            Console.WriteLine("View - Program");
            databaseService.ExecuteOperation("SELECT * FROM [IBA2024].[dbo].[StudentEmailsInCourse]");

            // Initialize the login service.
            var loginService = initializer.InitializeLoginService();
            Console.WriteLine("Attempting to login user 'user1'.");
            bool loginSuccess = loginService.Login("user1", "P@ssw0rd1");


            if (loginSuccess)
            {
                Console.WriteLine("Login successful.");
                Logger.Info("Login successful for user 'user1'.");
            }
            else
            {
                Console.WriteLine("Login failed.");
                Logger.Warn("Login failed for user 'user1'.");
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "An error occurred");
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        Logger.Info("Application ended");
    }
}










//using System;
//using Microsoft.Data.SqlClient;
//using System.Data;
//using SQLTest5.Modules.DBAdgang;
//using SQLTest5.Modules.StoredMethods;
//using SQLTest5.Modules.ViewDel;



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
//            databaseService.ExecuteOperation("SELECT * FROM [IBA2024].[dbo].[AllInstructorFullNames]");
//            Console.WriteLine("View - Program");
//            databaseService.ExecuteOperation("SELECT * FROM [IBA2024].[dbo].[AllInstructorFirstNames]");

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
