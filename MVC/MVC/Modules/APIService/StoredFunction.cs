using System.Collections.Generic;
using System;
using System.Data;

namespace MVC.Modules.APIService
{
    public class StoredFunction
    {
        public string Name { get; set; }
        public List<IDataParameter> Parameters { get; set; }
        public DbType ReturnType { get; set; } // DbType is commonly used to specify the column's data type in ADO.NET

        public StoredFunction(string name, List<IDataParameter> parameters, DbType returnType)
        {
            Name = name;
            Parameters = parameters;
            ReturnType = returnType;
        }
    }
}

