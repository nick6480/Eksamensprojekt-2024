// This class contains the main entry point of the program.
using SqlTest5.Modules;
using SQLTest5.Modules;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var configPath = "config.json"; // Path to the configuration file.
            var config = ConfigManager.LoadConfig(configPath); // Load configuration settings.
            var connectionManager = new SQLConnectionManager(config); // Initialize SQL connection manager.
            var queryExecutor = new SQLQueryExecutor(connectionManager); // Initialize SQL query executor.

            // Initialize ViewOperation class for performing view operations.
            var viewOperation = new ViewOperation(queryExecutor);
            var databaseService = new DatabaseService(viewOperation); // Initialize database service.

            // Example usage of the database service to perform view operations.
            Console.WriteLine("Executing operation on 'PersonView':");
            databaseService.ExecuteOperation("SELECT * FROM [Test22].[dbo].[GetSortNames]");
            Console.WriteLine("View - Program");
            databaseService.ExecuteOperation("SELECT * FROM [Test22].[dbo].[StudentEmails]");

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
