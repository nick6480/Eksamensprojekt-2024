using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace enterprise.login {
    internal class LoginHandler
    {
        // Method to authenticate a user
        public List<string> validation(string email, string password)
        {
            List<string> error_msg = new List<string>();



            if (string.IsNullOrWhiteSpace(email)) // Email must not be empty
            {
                error_msg.Add("Email må ikke være tom");
            }
            if (password.Length < 8) // Password must be at least  8 characters
            {
                error_msg.Add("Password skal have minimun 8 karaktere");
            }
            if (!Regex.IsMatch(password, @"[a-zA-Z]")) // Password must include at least 1 letter
            {
                error_msg.Add("Passworded skal have mindst 1 bogstav");
            }
            if (!Regex.IsMatch(password, @"\d")) // Password must include at least 1 digit 
            {
                error_msg.Add("Password skal have mindst 1 tal"); 
            }



            return error_msg;
        }

        public bool login(string username, string password)
        {
            if (DEBUG_CHECK_IF_USER_EXISTS(username, password))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        // Method to log out a user
        public void logout()
        {

        }

        // Method to check if a user is already logged in
        public bool isLoggedIn()
        {
            return false;
        }

        public bool DEBUG_CHECK_IF_USER_EXISTS(string username, string password)
        {
            if (username == "John" && password == "1234abcd")
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }

}
