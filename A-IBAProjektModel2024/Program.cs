using System;
using Microsoft.Data.SqlClient;
using System.Data;
using SQLTest5.Modules.DBAdgang;
using SQLTest5.Modules.StoredMethods;
using SQLTest5.Modules.ViewDel;



class Program
{
    static void Main(string[] args)
    {
        try
        {
            var initializer = new DatabaseInitializer("config.json");
            var databaseService = initializer.InitializeDatabaseService();

            // Example usage of the database service to perform view operations.
            Console.WriteLine("Executing operation on 'PersonView':");
            databaseService.ExecuteOperation("SELECT * FROM [IBA2024].[dbo].[AllInstructorFullNames]");
            Console.WriteLine("View - Program");
            databaseService.ExecuteOperation("SELECT * FROM [IBA2024].[dbo].[AllInstructorFirstNames]");

            // This structure allows you to easily switch to other operations like stored procedures or functions.
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}

