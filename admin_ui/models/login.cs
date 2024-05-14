using admin_ui.database;
using admin_ui.log;
using admin_ui.table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace admin_ui.login
{
    internal class LoginHandler
    {
        private string _currentUser;
        private DataHandler dataHandler;


        public LoginHandler()
        {
            dataHandler = new DataHandler();
        }

            // Method to authenticate a user
            public List<string> Validate(string email, string password)
        {
            List<string> error_msg = new List<string>();

            // Conditions for valid l
            if (string.IsNullOrWhiteSpace(email)) { error_msg.Add("Email må ikke være tom"); }
            if (password.Length < 8) { error_msg.Add("Password skal have minimum 8 karaktere"); }
            if (!Regex.IsMatch(password, @"[a-zA-Z]")){ error_msg.Add("Passwordet skal have mindst 1 bogstav"); }
            if (!Regex.IsMatch(password, @"\d")){ error_msg.Add("Password skal have mindst 1 tal"); }

            return error_msg;
        }

        // Method to perform login
        public bool Login(string username, string password)
        {
            bool isValid = false;
            isValid = dataHandler.AdminLogin(username, password);

            Debug.WriteLine($"IS THE PASSWORD VALID : {isValid}");

            if (isValid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method to log out a user
        public void Logout()
        {
            _currentUser = null; // Clear the current user
        }

        // Method to check if a user is already logged in
        public bool IsLoggedIn()
        {
            return _currentUser != null;
        }

        // Method to get the username of the logged-in user
        public string GetLoggedInUser()
        {
            return _currentUser;
        }

        // Debugging method to simulate user existence check
        private bool DEBUG_CHECK_IF_USER_EXISTS(string username, string password)
        {
            return username == "John" && password == "1234abcd";
        }
    }
}
