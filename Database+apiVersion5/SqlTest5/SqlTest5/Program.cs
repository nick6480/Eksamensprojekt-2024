using SqlTest5.Modules;
using SQLTest5.Modules;



class Program
{
    static void Main(string[] args)
    {
        try
        {
            var configPath = "config.json"; // Assume this path is correct and file exists.
            var config = ConfigManager.LoadConfig(configPath);
            var connectionManager = new SQLConnectionManager(config);
            var queryExecutor = new SQLQueryExecutor(connectionManager);

            // Assume we have a ViewOperation class that implements IDatabaseOperation.
            var viewOperation = new ViewOperation(queryExecutor);
            var databaseService = new DatabaseService(viewOperation);

            // Example usage of the database service to perform a view operation
            Console.WriteLine("Executing operation on 'PersonView':");
            databaseService.ExecuteOperation("SELECT * FROM Person");

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
