using System;
using System.IO;
using Newtonsoft.Json;

namespace TestAfAlt.Modules
{
    // Manages loading and validation of configuration settings from a JSON file.
    public class ConfigManager
    {
        [JsonProperty("DataSource")]
        public string DataSource { get; set; } = "defaultDataSource";  // Default value

        [JsonProperty("UserId")]
        public string UserId { get; set; } = "defaultUser";  // Default value

        [JsonProperty("Password")]
        public string Password { get; set; } = "defaultPassword";  // Default value

        [JsonProperty("InitialCatalog")]
        public string InitialCatalog { get; set; } = "defaultCatalog";  // Default value

        public static ConfigManager LoadConfig(string path)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine("Configuration file not found.");
                throw new FileNotFoundException("Configuration file not found.", path);
            }

            try
            {
                string json = File.ReadAllText(path);
                var config = JsonConvert.DeserializeObject<ConfigManager>(json);
                if (config == null)
                {
                    Console.Error.WriteLine("Failed to deserialize configuration. Check JSON format.");
                    throw new InvalidOperationException("Deserialization of configuration returned null.");
                }
                return config;
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine("Error parsing configuration: " + ex.Message);
                throw;
            }
        }
        // Loads configuration from a JSON file, throws exceptions if the file does not exist or cannot be parsed.
        //public static ConfigManager LoadConfig(string path)
        //{
        //    Console.WriteLine("We are here");
        //    if (!File.Exists(path))
        //    {
        //        Console.Error.WriteLine("Configuration file not found at path: " + path);
        //        throw new FileNotFoundException("Configuration file not found.", path);
        //    }

        //    string json = File.ReadAllText(path);
        //    try
        //    {
        //        var config = JsonConvert.DeserializeObject<ConfigManager>(json);
        //        if (config == null)
        //        {
        //            Console.Error.WriteLine("Failed to deserialize configuration. Check JSON format.");
        //            throw new InvalidOperationException("Deserialization of configuration returned null.");
        //        }
        //        return config;
        //    }
        //    catch (Newtonsoft.Json.JsonException ex)  // Fully qualify JsonException to prevent ambiguity
        //    {
        //        Console.Error.WriteLine("Error parsing configuration: " + ex.Message);
        //        throw new ApplicationException("JSON parsing error", ex);
        //    }
        //}
    }
}
