using System;
namespace TestAfAlt.Modules
{
    // Class representing stored functions
    public class StoredFunction
    {
        public string Name { get; set; }
        public string[] Parameters { get; set; }
        public string ReturnType { get; set; }

        // Constructor
        public StoredFunction(string name, string[] parameters, string returnType)
        {
            Console.WriteLine("We are here F");
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }
    }
}

