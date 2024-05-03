// See https://aka.ms/new-console-template for more information
using SQLTest2;

class Program
{
    static void Main(string[] args)
    {

        // Initializes the connection manager with connection details.
        SQLConnectionManager connectionManager = new SQLConnectionManager(
            "localhost", "sa", "dockerStrongPwd123", "test22");

        // Initializes the query executor.
        SQLQueryExecutor queryExecutor = new SQLQueryExecutor(connectionManager);
        // Initializes the database service.
        DatabaseService databaseService = new DatabaseService(queryExecutor);

            // Specify the table name from which data is to be retrieved
        databaseService.GetDataFromTable("Person"); // Retrieves data from 'Person' table and prints it.
        databaseService.GetDataFromTable("Credit"); // Example for another table.

        // Prints a closing message.
        Console.WriteLine("\nDone. Press enter.");
        Console.ReadLine();
    }
}
