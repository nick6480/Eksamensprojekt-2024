using System;
using System.Collections.Generic;
namespace MVC2.Modules
{
    public class StoredProcedure
    {
        public string? Name { get; set; }
        public string[]? Parameters { get; set; }
        public string? ReturnType { get; set; }

        public static string Execute(string procedureName)
        {
            return $"{procedureName} executed successfully.";
        }
    }
}

