using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    class Program
    {
        public static HttpListener listener = null;
        public static string url = "http://localhost:8000/";
        public static int requestCount = 0;
        public static string logFilePath = "request_logs.json"; // Path to JSON log file
        public static string htmlFilePath = "httpserver/login.html"; // Path to your HTML file

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

                    // Log request to JSON file
                    LogRequest(req);

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

        // Method to log request details to a JSON file
        static void LogRequest(HttpListenerRequest request)
        {
            string logEntry = $@"{{""Url"": ""{request.Url}"", ""Method"": ""{request.HttpMethod}"", ""UserHostName"": ""{request.UserHostName}"", ""UserAgent"": ""{request.UserAgent}"", ""AbsolutePath"": ""{request.Url.AbsolutePath}""}}";

            try
            {
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error logging request: " + ex.Message);
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
