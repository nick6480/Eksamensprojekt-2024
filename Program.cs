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
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string logFilePath = "request_logs.json"; // Sti til JSON-logfil

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

                    // If login request, validate credentials
                    if (req.Url.AbsolutePath == "/login" && req.HttpMethod == "POST")
                    {
                        string requestBody = await GetRequestPostData(req);
                        if (ValidateCredentials(requestBody))
                        {
                            // Send success response
                            byte[] data = Encoding.UTF8.GetBytes("Login successful");
                            resp.ContentType = "text/plain";
                            resp.ContentEncoding = Encoding.UTF8;
                            resp.ContentLength64 = data.LongLength;
                            await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        }
                        else
                        {
                            // Send failure response
                            byte[] data = Encoding.UTF8.GetBytes("Login failed. Invalid credentials.");
                            resp.ContentType = "text/plain";
                            resp.ContentEncoding = Encoding.UTF8;
                            resp.ContentLength64 = data.LongLength;
                            await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        }
                        resp.Close();
                    }
                    else
                    {
                        // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                        if (req.HttpMethod == "POST" && req.Url.AbsolutePath == "/localhost")
                        {
                            Console.WriteLine("Just got a click");
                            Console.WriteLine("body data");
                            Console.WriteLine(await GetRequestPostData(req));
                        }
                        else if (req.HttpMethod == "GET" && req.Url.AbsolutePath == "/get_data")
                        {
                            Console.WriteLine("Just got a click to get data");
                            // Handle GET request for data from SQL Server
                            byte[] responseData = await SQLCommunication.GetDataFromSQLServer();
                            await WriteResponse(resp, responseData);
                        }

                        // Make sure we don't increment the page views counter if `favicon.ico` is requested
                        if (req.Url.AbsolutePath != "/favicon.ico")
                            pageViews += 1;

                        // Write the response info
                        byte[] data = Encoding.UTF8.GetBytes("Request handled");
                        resp.ContentType = "text/plain";
                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = data.LongLength;

                        // Write out to the response stream (asynchronously), then close it
                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                        resp.Close();
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
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (var reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

        static async Task WriteResponse(HttpListenerResponse resp, byte[] data)
        {
            resp.ContentType = "application/json";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;
            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
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

