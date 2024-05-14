using System;
using System.Collections.Generic;
namespace MVC2.Modules
{
    public class StoredFunction
    {
        public string? Name { get; set; }
        public string[]? Parameters { get; set; }
        public string? ReturnType { get; set; }

        public static double ExecuteFunction(string functionName, object[] parameters)
        {
            if (parameters == null || parameters.Length < 2)
            {
                throw new ArgumentException("Insufficient parameters provided.");
            }

            // Safely convert parameters to double
            double firstParameter = Convert.ToDouble(parameters[0]);
            double secondParameter = Convert.ToDouble(parameters[1]);

            return firstParameter * secondParameter;
        }
    }
}

