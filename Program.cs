using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    class Program
    {
        public static HttpListener listener = null;
        public static string url = "http://localhost:8000/";
        public static int requestCount = 0;
        public static string htmlFilePath = Path.Combine(Environment.CurrentDirectory, "httpserver", "login.html");// Path to your HTML file

        // Method to validate password
        static bool ValidatePassword(string password)
        {
            // Check for minimum length
            if (password.Length < 8)
                return false;

            // Check for at least one letter, one digit, and one special character
            if (!Regex.IsMatch(password, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$"))
                return false;

            return true;
        }

        // Method to handle incoming connections asynchronously
        static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
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
                        string filename = req.Url.AbsolutePath.Substring(1); // Remove leading '/'
                        if (filename == "")
                        {
                            // Serve the HTML file if the root URL is requested
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
                        string password = await GetRequestPostData(req);

                        // Validate password
                        if (ValidatePassword(password))
                        {
                            // Password is valid, send success response
                            byte[] responseBytes = Encoding.UTF8.GetBytes("Password is valid.");
                            resp.ContentType = "text/plain";
                            resp.ContentLength64 = responseBytes.Length;
                            resp.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                        }
                        else
                        {
                            // Password is invalid, send error response
                            byte[] responseBytes = Encoding.UTF8.GetBytes("Password does not meet requirements.");
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
        static async Task<string> GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (Stream body = request.InputStream) // here we have data
            {
                using (var reader = new StreamReader(body, request.ContentEncoding))
                {
                    return await reader.ReadToEndAsync();
                }
            }
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

                    // Set appropriate content type based on file extension
                    resp.ContentType = GetContentType(filename);
                    resp.ContentLength64 = fileBytes.Length;
                    resp.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                    resp.OutputStream.Close();
                }
                else
                {
                    // If file doesn't exist, send a 404 error
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
            // Create and start the HTTP server
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle incoming requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }
}

