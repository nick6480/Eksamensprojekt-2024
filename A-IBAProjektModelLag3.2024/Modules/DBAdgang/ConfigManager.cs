using System;
using System.IO;
using Newtonsoft.Json;

namespace SQLTest5.Modules.DBAdgang
{
    /// <summary>
    /// The ConfigManager class is responsible for managing configuration settings
    /// needed for database operations. It can load these settings from a JSON file.
    /// </summary>
    public class ConfigManager
    {
        // Configuration properties with default values.

        /// <summary>
        /// The data source (e.g., server name) for the SQL connection.
        /// </summary>
        [JsonProperty("DataSource")]
        public string? DataSource { get; set; } = "defaultDataSource";  // Default value

        /// <summary>
        /// The user ID for the SQL connection.
        /// </summary>
        [JsonProperty("UserId")]
        public string? UserId { get; set; } = "defaultUser";  // Default value

        /// <summary>
        /// The password for the SQL connection.
        /// </summary>
        [JsonProperty("Password")]
        public string? Password { get; set; } = "defaultPassword";  // Default value

        /// <summary>
        /// The initial catalog (database name) for the SQL connection.
        /// </summary>
        [JsonProperty("InitialCatalog")]
        public string? InitialCatalog { get; set; } = "defaultCatalog";  // Default value

        /// <summary>
        /// Loads configuration settings from a JSON file.
        /// </summary>
        /// <param name="path">The path to the JSON configuration file.</param>
        /// <returns>An instance of ConfigManager with loaded settings.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the configuration file is not found.</exception>
        /// <exception cref="JsonException">Thrown if there is an error parsing the JSON configuration file.</exception>
        public static ConfigManager LoadConfig(string path)
        {
            // Check if the configuration file exists at the specified path.
            if (!File.Exists(path))
            {
                // Log an error message and throw an exception if the file is not found.
                Console.Error.WriteLine("Configuration file not found.");
                throw new FileNotFoundException("Configuration file not found.", path);
            }

            try
            {
                // Read the contents of the configuration file.
                string json = File.ReadAllText(path);

                // Deserialize the JSON content into a ConfigManager object.
                // If deserialization fails, return a new ConfigManager instance with default values.
                return JsonConvert.DeserializeObject<ConfigManager>(json) ?? new ConfigManager();
            }
            catch (JsonException ex)
            {
                // Log an error message and rethrow the exception if there is an error parsing the JSON file.
                Console.Error.WriteLine("Error parsing configuration: " + ex.Message);
                throw;
            }
        }
    }
}
