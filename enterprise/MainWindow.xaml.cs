using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace enterprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void login_btn_Click(object sender, RoutedEventArgs e) {
            string username;
            string password;

            username = username_login.Text;
            password = password_login.Text;

            Debug.WriteLine(username);
            Debug.WriteLine(password);
            
            LoginHandler loginHandler = new LoginHandler();

            if (loginHandler.login(username, password))
            {
                login_grid.Visibility = Visibility.Collapsed;
                dashboard_grid.Visibility = Visibility.Visible;
            } 
            else
            {
                login_error_msg.Content = "Username or password is incorrect";
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void create_user_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void new_user_name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void new_user_name_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}




public class LoginHandler
{
    // Method to authenticate a user
    public List<string> validation(string username, string password)
    {
        List<string> error_msg = new List<string>();


        if (string.IsNullOrWhiteSpace(username)) // Username must not be empty
        {
            error_msg.Add("Username cannot be empty");
        }
        if (password.Length < 8) // Password must be at least  8 characters
        {
            //error_msg.Add("Password must be at least  8 characters");
        }
        if (!Regex.IsMatch(password, @"[a-zA-Z]")) // Password must include at least 1 letter
        {
            //error_msg.Add("Password must contain at least one letter");
        }
        if (!Regex.IsMatch(password, @"\d")) // Password must include at least 1 letter 
        {
            //error_msg.Add("Password must contain at least one digit"); 
        }
           


        return error_msg;
    }

    public bool login(string username, string password)
    {
        if (DEBUG_CHECK_IF_USER_EXISTS(username, password))
        {
            return true;
        } else
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
        else { 
            return false;
        }
    }



}
