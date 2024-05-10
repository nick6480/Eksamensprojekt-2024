using Microsoft.Data.SqlClient;

namespace HttpListenerExample
{
    class Program
    {
        public static HttpListener listener = null;
        public static string url = "http://localhost:8000/";
        public static int requestCount = 0;
        public static string logFilePath = "request_logs.json"; // Path to JSON log file
        public static string htmlFilePath = "httpserver/login.html"; // Stien til din HTML-fil

        static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                if (listener != null)
                {
                    HttpListenerContext ctx = await listener.GetContextAsync();

                    // Peel out the requests and response objects
                    HttpListenerRequest req = ctx.Request;
                    HttpListenerResponse resp = ctx.Response;

                    // Print out some info about the request
                    Console.WriteLine("Request #: {0}", ++requestCount);
                    Console.WriteLine(req.Url.ToString());
                    Console.WriteLine(req.HttpMethod);
                    Console.WriteLine(req.UserHostName);
                    Console.WriteLine(req.UserAgent);
                    Console.WriteLine(req.Url.AbsolutePath);

                    // Log request to JSON file
                    LogRequest(req);

                    // Serve requested file or handle login request
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
                    else if (req.HttpMethod == "POST" && req.Url.AbsolutePath == "/login")
                    {
                        // Handle login request
                        string postData = await GetRequestPostData(req);
                        byte[] responseData = await HandleLogin(postData);

                        // Send response back to client
                        resp.ContentType = "application/json";
                        resp.ContentLength64 = responseData.Length;
                        resp.OutputStream.Write(responseData, 0, responseData.Length);
                        resp.OutputStream.Close();
                    }
                }
            }
        }

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

        static async Task<byte[]> HandleLogin(string postData)
        {
            // Your existing login handling logic here...
            return null;
        }

        static void ServeFile(string filename, HttpListenerResponse resp)
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, filename);

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    // Set the appropriate content type based on the file extension
                    resp.ContentType = GetContentType(filename);
                    resp.ContentLength64 = fileBytes.Length;
                    resp.OutputStream.Write(fileBytes, 0, fileBytes.Length);
                    resp.OutputStream.Close();
                }
                else
                {
                    // If the file doesn't exist, send a 404 error
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

        static void LogRequest(HttpListenerRequest request)
        {
            // Log the request details to a JSON file
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
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }
}

