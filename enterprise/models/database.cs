using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace enterprise.database
{
    internal class DbSettings
    {
        public string DbServerIp { get; set; }
        public string AuthType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Be cautious about storing passwords in plain text.

        /// <summary>
        /// Serializes the current object to a JSON string and saves it to a file.
        /// </summary>
        /// <param name="filePath">The file path where the JSON should be saved.</param>
        public void SaveToJson(string filePath)
        {
            // Serialize the current instance to a JSON string
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            // Write the JSON to a file
            File.WriteAllText(filePath, json);
        }
        public static DbSettings ReadFromJson(string filePath)
        {
            // Read the JSON text from the file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON text to a DbSettings object
            return JsonConvert.DeserializeObject<DbSettings>(json);
        }

        public void create()
        {

        }
    }
}
