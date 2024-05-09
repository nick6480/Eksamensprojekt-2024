using System;
using System.IO;
using Newtonsoft.Json;

namespace SQLTest5.Modules
{
    // Manages loading and validation of configuration settings from a JSON file.
    public class ConfigManager
    {
        [JsonProperty("DataSource")]
        public string? DataSource { get; set; } = "defaultDataSource";  // Default value

        [JsonProperty("UserId")]
        public string? UserId { get; set; } = "defaultUser";  // Default value

        [JsonProperty("Password")]
        public string? Password { get; set; } = "defaultPassword";  // Default value

        [JsonProperty("InitialCatalog")]
        public string? InitialCatalog { get; set; } = "defaultCatalog";  // Default value



        // Loads configuration from a JSON file, throws exceptions if the file does not exist or cannot be parsed.
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
                return JsonConvert.DeserializeObject<ConfigManager>(json) ?? new ConfigManager();  // Return a new instance with default values if deserialization fails
            }
            catch (JsonException ex)
            {
                Console.Error.WriteLine("Error parsing configuration: " + ex.Message);
                throw;
            }
        }
    }
}