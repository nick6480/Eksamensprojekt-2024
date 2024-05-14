﻿using System.Collections.Generic;
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
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.IO;




using enterprise.login;
using enterprise.table;
using enterprise.log;

using Xceed.Wpf.Toolkit;
using System.Data;
using enterprise.database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System;
using System.Windows.Forms;
using static enterprise.database.DataHandler;



namespace enterprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginHandler loginHandler;
        private TableStateMachine tableStateMachine;
        private LogHandler logHandler;
        private DataHandler dataHandler;
        private System.Windows.Controls.ComboBox new_student_course;

        public MainWindow()
        {
            InitializeComponent();

            loginHandler = new LoginHandler();
            tableStateMachine = new TableStateMachine();
            logHandler = new LogHandler();
            dataHandler = new DataHandler();


            this.updateSettings();
            this.update_data_grid();

        }

        // ---- LOGIN ----
        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string username;
            string password;

            username = username_login.Text;
            password = password_login.Text;

            if (loginHandler.Login(username, password))
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


        private void showOverlay(UIElement visibleOverlay) // Display overlay
        {
            overlay_grid.Visibility = Visibility.Visible;
            visibleOverlay.Visibility = Visibility.Visible;

        }
        private void hideOverlay() // Close overlay
        {
            overlay_grid.Visibility = Visibility.Collapsed;
            create_student_grid.Visibility = Visibility.Collapsed;
            create_teacher_grid.Visibility = Visibility.Collapsed;
            create_course_grid.Visibility = Visibility.Collapsed;
            create_room_grid.Visibility = Visibility.Collapsed;
        }

        private void close_overlay_btn_Click(object sender, RoutedEventArgs e) { hideOverlay(); } // Close overlay btn


        // Create Data btns
        private void create_student_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_student_grid); getStudentData();  }
        private void create_teacher_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_teacher_grid); }
        private void create_course_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_course_grid); getCourseData(); }
        private void create_room_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_room_grid); }


        private void createNew()
        {

        }

        // Create new Overlays
        private void save_new_student_Click(object sender, RoutedEventArgs e) // Create new student
        {
            string firstName = new_student_name.Text;
            string lastName = new_student_surname.Text;
            int? courseId = null;

            if (new_student_courses.SelectedItem != null)
            {
                string selectedCourse = ((ListBoxItem)new_student_courses.SelectedItem).Content.ToString();
                courseId = int.Parse(Regex.Match(selectedCourse, @"^(\d+)\.").Groups[1].Value);
            }










            string email = new_student_email.Text;
            string username = new_student_username.Text;
            string password = new_student_password.Password;


            List<string> errorMsg = loginHandler.Validate(email, password);

            //new_user_validation_error.Content = System.String.Join(Environment.NewLine, errorMsg);

            if (!errorMsg.Any()) // If no errors
            {
                dataHandler.createStudent(firstName, lastName, email, username, password, courseId);
            }









        }

        private void save_new_teacher_Click(object sender, RoutedEventArgs e) // Create new teacher
        {
            string firstName = new_teacher_name.Text;
            string lastName = new_teacher_surname.Text;
            string username = new_teacher_username.Text;
            string password = new_teacher_password.Password;

            List<string> errorMsg = loginHandler.Validate(username, password);

            //new_user_validation_error.Content = System.String.Join(Environment.NewLine, errorMsg);

            if (!errorMsg.Any()) // If no errors
            {
                dataHandler.createTeacher(firstName, lastName, username, password);
            }

        }

        private void save_new_course_Click(object sender, RoutedEventArgs e) // Create new course
        {
            string courseName = new_course_name.Text;
            int? teacherId = null;
            int? roomId = null;


            if (new_course_teacher.SelectedItem != null)
            {
                string selectedTeacher = ((ListBoxItem)new_course_teacher.SelectedItem).Content.ToString();

                // Split the input string by dot
                teacherId = int.Parse(Regex.Match(selectedTeacher, @"^(\d+)\.").Groups[1].Value);
            }

            if (new_course_room.SelectedItem != null)
            {
                string selectedRoom = ((ListBoxItem)new_course_room.SelectedItem).Content.ToString();
                roomId = int.Parse(Regex.Match(selectedRoom, @"^(\d+)\.").Groups[1].Value);
            }

            dataHandler.createCourse(courseName, teacherId, roomId);
        }




        private void populateListBox(System.Windows.Controls.ListBox listBox, string data)
        {
            listBox.Items.Clear();
            ListBoxItem itm = new ListBoxItem();
            itm.Content = data;

            listBox.Items.Add(itm);
        }

        private void getCourseData()
        {
            var rooms = dataHandler.getRooms();
            foreach (var room in rooms)
            {
                string roomData = $"{room.RoomId}. {room.RoomName}";
                populateListBox(new_course_room, roomData);
            }

            var teachers = dataHandler.getTeachers();
                foreach (var teacher in teachers)
                {
                    string teacherData = $"{teacher.TeacherId}. {teacher.FirstName} {teacher.LastName}";
                    populateListBox(new_course_teacher, teacherData);
                }
        }

        private void getStudentData()
        {
            var courses = dataHandler.getCourses();
            foreach (var course in courses)
            {
                string courseData = $"{course.CourseId}. {course.CourseName}";
                populateListBox(new_student_courses, courseData);
            }
        }













    private void save_new_room_Click(object sender, RoutedEventArgs e)
    {
        string roomName = new_room_name.Text;
        string roomAmount = new_room_amount_nr.Text;
        dataHandler.createRoom(roomName, Convert.ToInt32(roomAmount));
    }


















    // ---- Treeview display ----

    private void data_grid_state(object sender, RoutedEventArgs e)
    {
        //Button clickedBtn = (Button)sender;

        //tableStateMachine.setState(clickedBtn.Content);






    }



    // ---- DB SETTINGS ----


    private void checkSqlLogin()
    {
        if (db_auth_method_selector.Text == "SQL Login")
        {
            sql_username_textbox.IsReadOnly = false;
            sql_username_textbox.Foreground = Brushes.Black;

            sql_username_passwordbox.Focusable = true;
            sql_username_passwordbox.IsHitTestVisible = true;
            sql_username_passwordbox.Foreground = Brushes.Black;
        }
        else
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
        if (enable_db_settings_checkbox.IsChecked == false)
        {
            System.Windows.Controls.ComboBox db_auth_metod_selector = sender as System.Windows.Controls.ComboBox;
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

        settings.SaveToJson("settings.json");



        logHandler.NewLogEntry(loginHandler.GetLoggedInUser(), "Update", "DB Settings");
        update_data_grid();
    }

    private void updateSettings()
    {
        DbSettings settings = DbSettings.ReadFromJson("settings.json");

        db_server_ip_textbox.Text = settings.DbServerIp;
        db_auth_method_selector.Text = settings.AuthType;
        sql_username_textbox.Text = settings.Username;
        sql_username_passwordbox.Password = settings.Password;


    }


    private void update_data_grid()
    {
        log_datagrid.ItemsSource = null; // Clear grid
        log_datagrid.ItemsSource = logHandler.ReadLogEntries();


    }



    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void log_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void new_user_name_TextChanged(object sender, TextChangedEventArgs e)
    {

    }




    private void delete_selected_btn_Click(object sender, RoutedEventArgs e)
    {

    }



    private void new_student_name1_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void new_room_amount(object sender, TextCompositionEventArgs e)
    {

    }

    private void textboxOnlyInt(object sender, TextCompositionEventArgs e)
    {
        e.Handled = new Regex("[0^9]+").IsMatch(e.Text);
    }

    private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
    {

    }
}
}


