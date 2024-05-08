using System.Collections.Generic;
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
using System;
using System.Collections.Generic;




using enterprise.login;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace enterprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginHandler Login = new LoginHandler();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        // ---- LOGIN ----
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

        //---- Table ----
        private void create_user_btn_Click(object sender, RoutedEventArgs e)
        {
            overlay_grid.Visibility = Visibility.Visible;

        }

        private void close_overlay_btn_Click(object sender, RoutedEventArgs e)
        {
            overlay_grid.Visibility = Visibility.Collapsed;
        }

        private void save_new_user_Click(object sender, RoutedEventArgs e)
        {
            string email = new_user_email.Text;
            string password = new_user_password.Text;

            List<string> errorMsg = Login.validation(email, password);

            new_user_login_error.Content = System.String.Join(Environment.NewLine, errorMsg);

            if (!errorMsg.Any()) // If no errors
            {

            }

        }

    }
}



// wpf tool kit
// https://github.com/xceedsoftware/wpftoolkit/wiki 
