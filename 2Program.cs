using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SQLTest5.Modules.DBAdgang;

namespace HttpListenerExample
{
    class Program
    {
        public static HttpListener listener = null;
        public static string url = "http://localhost:8000/";
        public static string htmlFilePath = Path.Combine(Environment.CurrentDirectory, "httpserver", "login.html");
        public static string logFilePath = Path.Combine(Environment.CurrentDirectory, "httpserver", "requestLog.json");
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
                        string filename = req.Url.AbsolutePath.Substring(1);
                        if (filename == "")
                        {
                            ServeFile(htmlFilePath, resp);
                        }
                        else
                        {
                            ServeFile(filename, resp);
                        }
                    }
                    else if (req.HttpMethod == "POST")
                    {
                        // Handle POST requests
                        byte[] responseBytes;
                        if (req.Url.AbsolutePath == "/login")
                        {
                            string email = await GetRequestPostData(req, "email");
                            string password = await GetRequestPostData(req, "password");

                            // Authenticate user
                            bool isAuthenticated = AuthenticateUser(email, password);

                            if (isAuthenticated)
                            {
                                responseBytes = Encoding.UTF8.GetBytes("Login successful.");
                            }
                            else
                            {
                                responseBytes = Encoding.UTF8.GetBytes("Invalid email or password.");
                                resp.StatusCode = 400; // Bad Request
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
        static async Task<string> GetRequestPostData(HttpListenerRequest request, string paramName)
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
                    foreach (string pair in pairs)
                    {
                        string[] keyValue = pair.Split('=');
                        if (keyValue.Length > 1 && keyValue[0] == paramName)
                        {
                            return keyValue[1];
                        }
                    }
                }
            }
            return null;
        }

        // Method to serve requested files
        static void ServeFile(string filename, HttpListenerResponse resp)
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, filename);

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

                string json = JsonSerializer.Serialize(requestData);
                File.AppendAllText(logFilePath, json + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error logging request: " + ex.Message);
            }
        }
    }
}
