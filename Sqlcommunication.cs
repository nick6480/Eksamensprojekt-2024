using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    class SQLCommunication
    {
        public static async Task<byte[]> GetDataFromSQLServer(string query)
        {
            // Implement logic to connect to MS SQL Server and execute the provided query
            string connectionString = "Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;";

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
