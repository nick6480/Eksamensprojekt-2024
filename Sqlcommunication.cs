using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HttpListenerExample
{
    class SQLCommunication
    {
        public static async Task<byte[]> GetDataFromSQLServer()
        {
            // Implement logic to connect to MS SQL Server and fetch data from views
            string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User ID=myUsername;Password=myPassword;";
            string query = "SELECT * FROM myView";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        StringBuilder jsonData = new StringBuilder();
                        while (reader.Read())
                        {
                            // Append data to JSON string
                            // Example: jsonData.Append("{ \"name\": \"" + reader.GetString(0) + "\", \"age\": " + reader.GetInt32(1) + " }");
                        }
                        byte[] responseData = Encoding.UTF8.GetBytes(jsonData.ToString());
                        return responseData;
                    }
                }
            }
        }
    }
}
