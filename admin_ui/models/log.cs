using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


namespace admin_ui.log
{

    public enum Actions : int
    {
        Create  = 1,
        Update = 2,
        Delete = 3,
    }
    
    internal class LogEntry
    {

        public DateTime Timestamp { get; set; }
        public string Username { get; set; }
        //public Actions Action { get; set; }
        public string Action { get; set; }


        public string Target { get; set; }
    }

    internal class LogHandler
    {
        private string filePath = "log.json";  // Specify the path to your log file

        public void NewLogEntry(string username, string action, string target)
        {


            // Create a new log entry
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Username = username,
                Action = action,
                Target = target
            };

            // Read existing log entries
            var logEntries = ReadLogEntries();

            // Add new log entry to the list
            logEntries.Add(logEntry);

            // Write updated log entries back to the JSON file
            string json = JsonConvert.SerializeObject(logEntries, Formatting.Indented);
            // Write the JSON back to the file
            File.WriteAllText(filePath, json);
        }



        public List<LogEntry> ReadLogEntries()
        {
            string fname = "log.JSON";

            var json = File.ReadAllText(fname);
            List<LogEntry>? _logEntries = JsonConvert.DeserializeObject<List<LogEntry>>(json);

            return _logEntries;
        }


      
    }
}
    

