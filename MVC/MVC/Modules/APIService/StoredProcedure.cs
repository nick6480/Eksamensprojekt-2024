using System;
namespace MVC.Modules.APIService
{
    public class StoredProcedure
    {
        public string Name { get; set; }
        public string[] Parameters { get; set; }
        public string ReturnType { get; set; }
    }
}

