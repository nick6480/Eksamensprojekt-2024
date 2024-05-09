using System;
namespace TestAfAlt.Modules
{
    // Class representing database views
    public class DatabaseView
    {
        public string Name { get; set; }
        public string Query { get; set; }

        // Constructor
        public DatabaseView(string name, string query)
        {
            Name = name;
            Query = query;
        }
    }
}

