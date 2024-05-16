using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using admin_ui.login;
using admin_ui.log;
using admin_ui.data;
using Newtonsoft.Json;
using System.Data;




namespace admin_ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginHandler loginHandler;
        private LogHandler logHandler;
        private DataHandler dataHandler;
        private System.Windows.Controls.ComboBox new_student_course;

        public MainWindow()
        {
            InitializeComponent();

            loginHandler = new LoginHandler();
            logHandler = new LogHandler();
            dataHandler = new DataHandler();


            updateSettings();
            update_data_grid();
            getStudents();
            hideOverlay();
        }

        // ---- LOGIN ----
        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string username;
            string password;

            username = username_login.Text;
            password = password_login.Password;

            if (loginHandler.Login(username, password))
            {

                login_grid.Visibility = Visibility.Collapsed;
                dashboard_grid.Visibility = Visibility.Visible;
            }
            else
            {
                login_error_msg.Content = "Brugernavn eller password er forkert";
            }

        }

        //---- Table ----
        private void getStudents() // Display overlay
        {
            dataTable.ItemsSource = null;

            string jsonData = dataHandler.getData("StudentInfo", "StudentId, FirstName, LastName, Brugernavn, MailAdress, KursusNavn");
            if (jsonData != null)
            {
                // Deserialize JSON string to DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
                dataTable.ItemsSource = dt.DefaultView;
            }
        }

        private void getTeachers() // Display overlay
        {
            string jsonData = dataHandler.getData("TeacherInfo", "FirstName, LastName, Brugernavn, KursusNavn, LokaleNavn");
            if (jsonData != null)
            {
                // Deserialize JSON string to DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
                dataTable.ItemsSource = dt.DefaultView;
            }
        }

        private void getCourses() // Display overlay
        {
            string jsonData = dataHandler.getData("CourseInfo", "*");
            if (jsonData != null)
            {
                // Deserialize JSON string to DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
                dataTable.ItemsSource = dt.DefaultView;
            }
        }















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
            create_admin_grid.Visibility = Visibility.Collapsed;
        }

        private void close_overlay_btn_Click(object sender, RoutedEventArgs e) { hideOverlay(); } 
        private void create_student_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_student_grid); getStudentData();  }
        private void create_teacher_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_teacher_grid); }
        private void create_course_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_course_grid); getCourseData(); }
        private void create_room_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_room_grid); }
        private void create_admin_btn_Click(object sender, RoutedEventArgs e) { showOverlay(create_admin_grid); }


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
                logHandler.NewLogEntry(loginHandler.GetLoggedInUser(), "Create", $"Student: {firstName}");
                update_data_grid();
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
                logHandler.NewLogEntry(loginHandler.GetLoggedInUser(), "Create", $"Teacher: {firstName}");
                update_data_grid();
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
            logHandler.NewLogEntry(loginHandler.GetLoggedInUser(), "Create", $"Course: {courseName}");
            update_data_grid();
        }


        private void save_new_room_Click(object sender, RoutedEventArgs e)
        {
            string roomName = new_room_name.Text;
            string roomAmount = new_room_amount_nr.Text;
            dataHandler.createRoom(roomName, Convert.ToInt32(roomAmount));
            logHandler.NewLogEntry(loginHandler.GetLoggedInUser(), "Create", $"Room: {roomName}");
            update_data_grid();
        }


        private void save_new_admin_Click(object sender, RoutedEventArgs e) // Create new teacher
        {
            string username = new_admin_username.Text;
            string password = new_admin_password.Password;

            List<string> errorMsg = loginHandler.Validate(username, password);

            //new_user_validation_error.Content = System.String.Join(Environment.NewLine, errorMsg);

            if (!errorMsg.Any()) // If no errors
            {
                dataHandler.createAdmin(username, password);
                logHandler.NewLogEntry(loginHandler.GetLoggedInUser(), "Create", $"Admin: {username}");
                update_data_grid();
            }

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



        // ---- Treeview display ----

        private void data_grid_state(object sender, RoutedEventArgs e)
        {
            getStudentData();
        }

       // ---- DB SETTINGS ----


        private void checkSqlLogin()
        {
            if (db_auth_method_selector.Text == "SQL Login")
            {
                sql_username_textbox.IsEnabled = true;
                sql_username_passwordbox.IsEnabled = true;
            }
            else
            {
                sql_username_textbox.IsEnabled = false;
                sql_username_passwordbox.IsEnabled = false;
            }
        }

        private void enableDbSettings(object sender, RoutedEventArgs e) // ENABLE DB SETTINGS  EDITING
        {
            Debug.WriteLine("CHECKED");
            db_server_ip_textbox.IsEnabled = true;
            db_auth_method_selector.IsEnabled = true;

            save_db_settings_btn.Visibility = Visibility.Visible;

            checkSqlLogin();


        }
        private void disableDbSettings(object sender, RoutedEventArgs e) // DISABLE DB SETTINGS EDITING
        {
            Debug.WriteLine("UNCHECKED");
            db_server_ip_textbox.IsEnabled = false;
            db_auth_method_selector.IsEnabled = false;
            sql_username_textbox.IsEnabled = false;
            sql_username_passwordbox.IsEnabled = false;

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

        private void textboxOnlyInt(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[0^9]+").IsMatch(e.Text);
        }

        private void display_students(object sender, RoutedEventArgs e)
        {
            getStudents();
        }
        private void display_teachers(object sender, RoutedEventArgs e)
        {
            getTeachers();
        }

        private void display_courses(object sender, RoutedEventArgs e)
        {
            getCourses();
        }

    }
}


