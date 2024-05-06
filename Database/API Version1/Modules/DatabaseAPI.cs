using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient; // Tilføj dette for SqlParameter brug

namespace TestAfAlt.Modules
{

    public class DatabaseAPI : IDatabaseOperations
    {
        private List<StoredProcedure> storedProcedures;
        private List<StoredFunction> storedFunctions;
        private List<DatabaseView> databaseViews;
        private readonly string _connectionString;
        

        public DatabaseAPI(string connectionString)
        {
            _connectionString = connectionString;
            storedProcedures = new List<StoredProcedure>();
            storedFunctions = new List<StoredFunction>();
            databaseViews = new List<DatabaseView>();
        }

        public void AddStoredProcedure(StoredProcedure storedProcedure)
        {
            Console.WriteLine("We are here APP");
            storedProcedures.Add(storedProcedure);
        }

        public void AddStoredFunction(StoredFunction storedFunction)
        {
            Console.WriteLine("We are here AFF");
            storedFunctions.Add(storedFunction);
        }

        public void AddDatabaseView(DatabaseView databaseView)
        {
            Console.WriteLine("We are here AVV");
            databaseViews.Add(databaseView);
        }

        public void GetDataFromTable(string tableName)
        {
            Console.WriteLine("We are here GT");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM {tableName}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                            }
                        }
                    }
                }
            }
        }

        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            Console.WriteLine("We are here EPP");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalarFunction(string functionName, SqlParameter[] parameters)
        {
            Console.WriteLine("We are here EFF");
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(functionName, connection) { CommandType = CommandType.StoredProcedure };
                command.Parameters.AddRange(parameters);
                return command.ExecuteScalar();
            }
        }

        public List<StoredProcedure> GetStoredProcedures()
        {
            Console.WriteLine("We are here GST");
            return storedProcedures;
        }

        public List<StoredFunction> GetStoredFunctions()
        {
            Console.WriteLine("We are here GFU");
            return storedFunctions;
        }

        public List<DatabaseView> GetDatabaseViews()
        {
            Console.WriteLine("We are here GVI");
            return databaseViews;
        }
    }
}
//    // Class representing the middleware component
//    public class DatabaseAPI : IDatabaseOperations
//    {
//        // List to store stored procedures
//        private List<StoredProcedure> storedProcedures;
//        // List to store stored functions
//        private List<StoredFunction> storedFunctions;
//        // List to store database views
//        private List<DatabaseView> databaseViews;

//        // Constructor
//        public DatabaseAPI()
//        {
//            storedProcedures = new List<StoredProcedure>();
//            storedFunctions = new List<StoredFunction>();
//            databaseViews = new List<DatabaseView>();
//        }

//        // Method to add a stored procedure
//        public void AddStoredProcedure(StoredProcedure storedProcedure)
//        {
//            storedProcedures.Add(storedProcedure);
//        }

//        // Method to add a stored function
//        public void AddStoredFunction(StoredFunction storedFunction)
//        {
//            storedFunctions.Add(storedFunction);
//        }

//        // Method to add a database view
//        public void AddDatabaseView(DatabaseView databaseView)
//        {
//            databaseViews.Add(databaseView);
//        }

//        public void GetDataFromTable(string tableName)
//        {
//            Console.WriteLine($"Fetching data from {tableName}...");
//            // Implementer din logik til at hente data her
//        }

//        public void ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
//        {
//            Console.WriteLine($"Executing stored procedure {procedureName} with parameters {parameters.Length}");
//            // Logik til at udføre den angivne stored procedure kan tilføjes her
//        }

//        public object ExecuteScalarFunction(string functionName, SqlParameter[] parameters)
//        {
//            Console.WriteLine($"Executing scalar function {functionName} with parameters {parameters.Length}");
//            // Logik til at returnere værdi fra en scalar function kan tilføjes her
//            return null; // Dette bør ændres til at returnere en faktisk værdi fra funktionen
//        }

//        // Getter methods for stored procedures, stored functions, and database views
//        public List<StoredProcedure> GetStoredProcedures()
//        {
//            return storedProcedures;
//        }

//        public List<StoredFunction> GetStoredFunctions()
//        {
//            return storedFunctions;
//        }

//        public List<DatabaseView> GetDatabaseViews()
//        {
//            return databaseViews;
//        }
//    }
//}
