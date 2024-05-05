// Class representing stored procedures
public class StoredProcedure
{
    public string Name { get; set; }
    public string[] Parameters { get; set; }
    public string ReturnType { get; set; }

    // Constructor
    public StoredProcedure(string name, string[] parameters, string returnType)
    {
        Name = name;
        Parameters = parameters;
        ReturnType = returnType;
    }
}

// Class representing stored functions
public class StoredFunction
{
    public string Name { get; set; }
    public string[] Parameters { get; set; }
    public string ReturnType { get; set; }

    // Constructor
    public StoredFunction(string name, string[] parameters, string returnType)
    {
        Name = name;
        Parameters = parameters;
        ReturnType = returnType;
    }
}

// Class representing database views
public class DatabaseView
{
    public string Name { get; set; }
    public string Query { get; set; }

    // Constructor
    public DatabaseView(string name, string query)
    {
        Name = name;
        Query = query;
    }
}

// Class representing the middleware component
public class DatabaseMiddleware
{
    // List to store stored procedures
    private List<StoredProcedure> storedProcedures;
    // List to store stored functions
    private List<StoredFunction> storedFunctions;
    // List to store database views
    private List<DatabaseView> databaseViews;

    // Constructor
    public DatabaseMiddleware()
    {
        storedProcedures = new List<StoredProcedure>();
        storedFunctions = new List<StoredFunction>();
        databaseViews = new List<DatabaseView>();
    }

    // Method to add a stored procedure
    public void AddStoredProcedure(StoredProcedure storedProcedure)
    {
        storedProcedures.Add(storedProcedure);
    }

    // Method to add a stored function
    public void AddStoredFunction(StoredFunction storedFunction)
    {
        storedFunctions.Add(storedFunction);
    }

    // Method to add a database view
    public void AddDatabaseView(DatabaseView databaseView)
    {
        databaseViews.Add(databaseView);
    }

    // Getter methods for stored procedures, stored functions, and database views
    public List<StoredProcedure> GetStoredProcedures()
    {
        return storedProcedures;
    }

    public List<StoredFunction> GetStoredFunctions()
    {
        return storedFunctions;
    }

    public List<DatabaseView> GetDatabaseViews()
    {
        return databaseViews;
    }
}
