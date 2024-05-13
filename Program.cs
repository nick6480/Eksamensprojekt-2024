using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HttpListenerExample
{
    class Program
    {
        public static HttpListener listener = null;
        public static string url = "http://localhost:8000/";
        public static int requestCount = 0;
        public static string htmlFilePath = Path.Combine(Environment.CurrentDirectory, "httpserver", "login.html");
        public static string connectionString = "Data Source=localhost;Initial Catalog=dbo;User ID=sa;Password=dockerStrongPwd123;";

        // Method to validate user against the database
        static bool ValidateUser(string email, string password)
        {
            string query = "SELECT COUNT(*) FROM dbo.person WHERE email = @Email AND password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    connection.Close();

                    return count > 0;
                }
            }
        }

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
                    Console.WriteLine("Request #: {0}", ++requestCount);
                    Console.WriteLine(req.Url.ToString());
                    Console.WriteLine(req.HttpMethod);
                    Console.WriteLine(req.UserHostName);
                    Console.WriteLine(req.UserAgent);
                    Console.WriteLine(req.Url.AbsolutePath);

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
                        string email = await GetRequestPostData(req, "email");
                        string password = await GetRequestPostData(req, "password");

                        // Validate user
                        if (ValidateUser(email, password))
                        {
                            // Send success response
                            byte[] responseBytes = Encoding.UTF8.GetBytes("Login successful.");
                            resp.ContentType = "text/plain";
                            resp.ContentLength64 = responseBytes.Length;
                            resp.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                        }
                        else
                        {
                            // Send error response
                            byte[] responseBytes = Encoding.UTF8.GetBytes("Invalid email or password.");
                            resp.ContentType = "text/plain";
                            resp.ContentLength64 = responseBytes.Length;
                            resp.StatusCode = 400; // Bad Request
                            resp.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                        }

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

        static void Main(string[] args)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            listener.Close();
        }
    }
}


