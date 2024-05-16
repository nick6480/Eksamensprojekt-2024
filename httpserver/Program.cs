using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using SQLTest5.Modules.DBAdgang;

namespace HttpListenerExample
{
    class Program
    {
        public static HttpListener listener = null;
        public static string url = "http://localhost:8000/";
        // public static string htmlFilePath = Path.Combine(Environment.CurrentDirectory, "httpserver", "login.html");
        public static string loginFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "views", "login.html");
        public static string teacherFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "views", "underviser.html");
        public static string studentsFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "views", "studerende.html");



        public static string logFilePath = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "logs", "requestLog.json");
        public static List<RequestLogEntry> requestLog = new List<RequestLogEntry>();

        // Method to handle incoming connections asynchronously
        static async Task HandleIncomingConnections()
        {
            while (true)
            {
                if (listener != null)
                {
                    // Wait for an incoming HTTP request
                    HttpListenerContext ctx = await listener.GetContextAsync();

                    // Extract request and response objects
                    HttpListenerRequest req = ctx.Request;
                    HttpListenerResponse resp = ctx.Response;

                    // Log request details
                    LogRequest(req);

                    // Serve requested file
                    if (req.HttpMethod == "GET")
                    {
                        byte[] responseBytes;
                        string filename = req.Url.AbsolutePath.Substring(1);
                        Console.WriteLine(filename);    
                        
                        if (filename == "") // Route index
                        {
                            ServeFile(loginFilePath, resp);
                        }
                        else if (filename == "studerende") // Route studerende
                        {
                            ServeFile(studentsFilePath, resp);
                        }
                        else if (filename == "underviser") // Route underviser
                        {
                            ServeFile(teacherFilePath, resp);
                        }
                        else if (filename == "getStudentData") // Get Student data
                        {
                            var queryParameters = req.Url.Query;
                            string username = HttpUtility.ParseQueryString(queryParameters).Get("username"); // Get username from querey

                            DataTable result = CallViewWithUsername("UserDetailsView", "FirstName, LastName, MailAdress, KursusNavn, LokaleNavn", username);
                            string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
                           

                            responseBytes = Encoding.UTF8.GetBytes(jsonResult);
                            resp.ContentType = "text/plain";
                            resp.ContentLength64 = responseBytes.Length;
                            await resp.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                            resp.OutputStream.Close();

                        }
                        else if (filename == "getTeacherData") // Get Teacher data
                        {

                            var queryParameters = req.Url.Query;

                            string username = HttpUtility.ParseQueryString(queryParameters).Get("username");

                            DataTable teacherDetailsResult = CallViewWithUsername("TeacherDetailsView", "FirstName, LastName, KursusNavn, LokaleNavn, StudentNames", username);
                            string jsonResult = JsonConvert.SerializeObject(teacherDetailsResult, Formatting.Indented);
                           
                            responseBytes = Encoding.UTF8.GetBytes(jsonResult);
                            resp.ContentType = "text/plain";
                            resp.ContentLength64 = responseBytes.Length;
                            await resp.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                            resp.OutputStream.Close();

                        }

                        else
                        {
                            ServeFile(filename, resp);
                        }
                    }
                    else if (req.HttpMethod == "POST") // Handle POST requests
                    {
                        
                        byte[] responseBytes;
                        if (req.Url.AbsolutePath == "/login")
                        {
                            Console.WriteLine("NEW REQUEST");
                            var postData = await GetRequestPostData(req);
                            string email = postData.ContainsKey("email") ? postData["email"] : null;
                            string password = postData.ContainsKey("password") ? postData["password"] : null;

                            

                            // Authenticate user
                            string isAuthenticated = AuthenticateUser(email, password);

                            if (isAuthenticated != null)
                            {
                                // Authentication successful (user role retrieved)
                                
                                responseBytes = Encoding.UTF8.GetBytes(isAuthenticated);
                            }
                            else
                            {
                                // Authentication failed (user not found or no role)
                                responseBytes = Encoding.UTF8.GetBytes("Invalid email or password.");
                            }

                        }
                        else
                        {
                            responseBytes = Encoding.UTF8.GetBytes("Invalid endpoint.");
                            resp.StatusCode = 404; // Not Found
                        }

                        resp.ContentType = "text/plain";
                        resp.ContentLength64 = responseBytes.Length;
                        await resp.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        resp.OutputStream.Close();
                    }
                }
            }
        }

        // Method to extract POST data from the request
        static async Task<Dictionary<string, string>> GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (Stream body = request.InputStream)
            {
                using (var reader = new StreamReader(body, request.ContentEncoding))
                {
                    string postData = await reader.ReadToEndAsync();
                    string[] pairs = postData.Split('&');
                    var data = new Dictionary<string, string>();
                    foreach (string pair in pairs)
                    {
                        string[] keyValue = pair.Split('=');
                        if (keyValue.Length > 1)
                        {
                            data[keyValue[0]] = keyValue[1];
                        }
                    }
                    return data;
                }
            }
        }


        static void ServeFile(string filename, HttpListenerResponse resp)
        {
            try
            {
                // Ensure the filename starts with the "views" folder
                string filePath = Path.Combine(Environment.CurrentDirectory, "views", filename);

                if (File.Exists(filePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    resp.ContentType = GetContentType(filename);
                    resp.ContentLength64 = fileBytes.Length;
                    resp.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                    resp.OutputStream.Close();
                }
                else
                {
                    resp.StatusCode = 404;
                    resp.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error serving file: " + ex.Message);
                resp.StatusCode = 500;
                resp.Close();
            }
        }



        // Method to determine content type based on file extension
        static string GetContentType(string filename)
        {
            switch (Path.GetExtension(filename).ToLowerInvariant())
            {
                case ".html":
                    return "text/html";
                case ".css":
                    return "text/css";
                case ".js":
                    return "text/javascript";
                default:
                    return "application/octet-stream";
            }
        }

        // Method to log request details
        static void LogRequest(HttpListenerRequest request)
        {
            RequestLogger logger = new RequestLogger(logFilePath);
            logger.LogRequest(request);
        }

        static DataTable CallViewWithUsername(string viewName, string columns, string username) 
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = "DESKTOP-T66VN3N\\SQLEXPRESS", // SHOULD READ FROM CONFIG FILE
                UserID = "nick",
                Password = "1234Abcd#",
                InitialCatalog = "eksammensprojekt2024",
                TrustServerCertificate = true
            };

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                string query = $"SELECT {columns} FROM {viewName} WHERE Brugernavn = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    connection.Open();
                    DataTable result = new DataTable();
                    result.Load(command.ExecuteReader());
                    return result;
                }
            }
        }



        // Method to determine content type based on file extension
        static string AuthenticateUser(string email, string password) // TEMP -- COULD NOT GET THE PROPER WAY TO WORK IN TIME
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder // SHOULD READ FROM CONFIG FILE
            {
                DataSource = "DESKTOP-T66VN3N\\SQLEXPRESS",
                UserID = "nick",
                Password = "1234Abcd#",
                InitialCatalog = "eksammensprojekt2024",
                TrustServerCertificate = true
            };

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) 
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CheckLoginAndGetRole", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.Add("@Brugernavn", SqlDbType.NVarChar, 50).Value = email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = password;

                    // Output parameter for Role
                    SqlParameter roleParameter = new SqlParameter("@Role", SqlDbType.NVarChar, 50);
                    roleParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(roleParameter);

                    // Execute command
                    command.ExecuteNonQuery();

                    // Retrieve output parameter value
                    string role = Convert.ToString(command.Parameters["@Role"].Value);

                    Console.WriteLine("Role: " + role);
                    return role;
                }
            }
        }



        static async Task Main(string[] args)
            {
                listener = new HttpListener();
                listener.Prefixes.Add(url);
                listener.Start();
                Console.WriteLine("Listening for connections on {0}", url);
                await HandleIncomingConnections();
            }
        }

        // Define a model class for log data
        public class RequestLogEntry
        {
            public DateTime Timestamp { get; set; }
            public string HttpMethod { get; set; }
            public string Url { get; set; }
            public string UserHostName { get; set; }
            public string UserAgent { get; set; }
        }

        public class RequestLogger
        {
            private string logFilePath;

            public RequestLogger(string logFilePath)
            {
                this.logFilePath = logFilePath;
            }

            public void LogRequest(HttpListenerRequest request)
            {
                try
                {
                    var requestData = new
                    {
                        Timestamp = DateTime.UtcNow,
                        URL = request.Url.ToString(),
                        HttpMethod = request.HttpMethod,
                        UserHostName = request.UserHostName,
                        UserAgent = request.UserAgent
                    };

                    string json = System.Text.Json.JsonSerializer.Serialize(requestData);
                    File.AppendAllText(logFilePath, json + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error logging request: " + ex.Message);
                }
            }
        }
    }


// THE ORIGINAL AuthenticateUser

/*
// Database access code 
 static bool AuthenticateUser(string email, string password)
{
    try
    {
        var initializer = new DatabaseInitializer("config.json");
        var databaseService = initializer.InitializeDatabaseService();

        // Query the database to validate user credentials
        string query = "SELECT COUNT(*) FROM dbo.Users WHERE Email = @Email AND Password = @Password";
        var parameters = new SqlParameter[]
        {
    new SqlParameter("@Email", email),
    new SqlParameter("@Password", password)
        };

        // Execute the query using ExecuteOperation method
        databaseService.ExecuteOperation(query, parameters);

        // Since ExecuteOperation does not return anything, assume authentication is successful
        return true;
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"An error occurred during authentication: {ex.Message}");
        return false;
    }
}

*/