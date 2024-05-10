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
using enterprise.table;
using Xceed.Wpf.Toolkit;
using System.Data;
using enterprise.database;



namespace enterprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private LoginHandler loginHandler;
        private TableStateMachine tableStateMachine;



        public MainWindow()
        {
            InitializeComponent();

            loginHandler = new LoginHandler();
            tableStateMachine = new TableStateMachine();

            this.updateSettings();

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

            List<string> errorMsg = loginHandler.validation(email, password);

            new_user_login_error.Content = System.String.Join(Environment.NewLine, errorMsg);

            if (!errorMsg.Any()) // If no errors
            {

            }

        }

        // ---- Treeview display ----

        private void data_grid_state(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = (Button)sender;

            //tableStateMachine.setState(clickedBtn.Content);
            
        }



        // ---- DB SETTINGS ----


        private void checkSqlLogin()
        {
            if  (db_auth_method_selector.Text == "SQL Login")
            {
                sql_username_textbox.IsReadOnly = false;
                sql_username_textbox.Foreground = Brushes.Black;

                sql_username_passwordbox.Focusable = true;
                sql_username_passwordbox.IsHitTestVisible = true;
                sql_username_passwordbox.Foreground = Brushes.Black;
            } else
            {
                sql_username_textbox.IsReadOnly = true;
                sql_username_textbox.Foreground = Brushes.Gray;

                sql_username_passwordbox.Focusable = false;
                sql_username_passwordbox.IsHitTestVisible = false;
                sql_username_passwordbox.Foreground = Brushes.Gray;
            }
           
        }



        private void enableDbSettings(object sender, RoutedEventArgs e) // ENABLE DB SETTINGS  EDITING
        {
            Debug.WriteLine("CHECKED"); 
            db_server_ip_textbox.IsReadOnly = false;
            db_server_ip_textbox.Foreground = Brushes.Black;
            db_auth_method_selector.Foreground = Brushes.Black;

            save_db_settings_btn.Visibility = Visibility.Visible;

            this.checkSqlLogin();


        }
        private void disableDbSettings(object sender, RoutedEventArgs e) // DISABLE DB SETTINGS EDITING
        {
            Debug.WriteLine("UNCHECKED");
            db_server_ip_textbox.IsReadOnly = true;
            db_server_ip_textbox.Foreground = Brushes.Gray;
            db_auth_method_selector.Foreground = Brushes.Gray;


            sql_username_textbox.IsReadOnly = true;
            sql_username_textbox.Foreground = Brushes.Gray;

            sql_username_passwordbox.Focusable = false;
            sql_username_passwordbox.IsHitTestVisible = false;
            sql_username_passwordbox.Foreground = Brushes.Gray;

            save_db_settings_btn.Visibility = Visibility.Collapsed;


        }

        private void checkIfDropdownAllowed(object sender, EventArgs e)
        {
            if (enable_db_settings_checkbox.IsChecked == false) {
                ComboBox db_auth_metod_selector = sender as ComboBox;
                db_auth_metod_selector.IsDropDownOpen = false;
            }
        }



        private void updateSelection(object sender, EventArgs e)
        {
            this.checkSqlLogin();
        }

        private void save_db_settings_btn_Click(object sender, RoutedEventArgs e)
        {
            DbSettings settings = new DbSettings
            {
                DbServerIp = db_server_ip_textbox.Text,
                AuthType = db_auth_method_selector.Text,
                Username = sql_username_textbox.Text,
                Password = sql_username_passwordbox.Password
            };

            settings.SaveToJson(@"settings.json");

        }

        private void updateSettings()
        {
            DbSettings settings = DbSettings.ReadFromJson("settings.json");

            db_server_ip_textbox.Text = settings.DbServerIp;
            db_auth_method_selector.Text = settings.AuthType;
            sql_username_textbox.Text = settings.Username;
            sql_username_passwordbox.Password = settings.Password;


        }

    }
}



// wpf tool kit
// https://github.com/xceedsoftware/wpftoolkit/wiki 
