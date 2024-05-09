using System;
namespace TestAfAlt.Modules
{
    // Class representing stored procedures
    public class StoredProcedure
    {
        public string Name { get; set; }
        public string[] Parameters { get; set; }
        public string ReturnType { get; set; }

        // Constructor
        public StoredProcedure(string name, string[] parameters, string returnType)
        {
            Console.WriteLine("We are here p");
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }
    }
}

