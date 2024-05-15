using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Web;


namespace admin_ui.database
{
    internal class DbSettings
    {
        public string DbServerIp { get; set; }
        public string AuthType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } 

        /// <summary>
        /// Serializes the current object to a JSON string and saves it to a file.
        /// </summary>
        /// <param name="filePath">The file path where the JSON should be saved.</param>
        public void SaveToJson(string filePath)
        {
            // Serialize the current instance to a JSON string
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            // Write the JSON to a file
            File.WriteAllText(filePath, json);
        }
        public static DbSettings ReadFromJson(string filePath)
        {
            // Read the JSON text from the file
            string json = File.ReadAllText(filePath);

            // Deserialize the JSON text to a DbSettings object
            return JsonConvert.DeserializeObject<DbSettings>(json);
        }
    }
    internal class DataHandler
    {
        private SqlConnectionStringBuilder msSqlConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();


            DbSettings settings = DbSettings.ReadFromJson("settings.json");

            builder.DataSource = settings.DbServerIp;
            builder.UserID = settings.Username;
            builder.Password = settings.Password;

            if (settings.AuthType == "Windows Authentication")
            {
                builder.IntegratedSecurity = true;
            }
            builder.InitialCatalog = "eksammensprojekt2024";
            builder.TrustServerCertificate = true;

            return builder;
        }



        public void createStudent(string firstName, string lastName, string email, string username, string password, int? courseId)
        {
            string encodedPassword = HttpUtility.UrlEncode(password);
            try
            {

                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();


                    // Create a SqlCommand object for calling the stored procedure
                    using (SqlCommand command = new SqlCommand("CreateStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@MailAddress", email);
                        command.Parameters.AddWithValue("@Password", encodedPassword);
                        command.Parameters.AddWithValue("@Brugernavn", username);
                        command.Parameters.AddWithValue("@KursusId ", courseId);


                        // Execute the command
                        int personId = (int)command.ExecuteScalar();

                        Debug.WriteLine("New user created with PersonId: " + personId);
                    }






                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public void createRoom(string roomName, int roomAmount)
        {
            try
            {

                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();


                    // Create a SqlCommand object for calling the stored procedure
                    using (SqlCommand command = new SqlCommand("CreateRoom", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@LokaleNavn", roomName);
                        command.Parameters.AddWithValue("@Antal", roomAmount);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    



        public void createCourse(string courseName, int? courseTeacerId, int? courseRoomId)
        {
            try
            {

                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();


                    // Create a SqlCommand object for calling the stored procedure
                    using (SqlCommand command = new SqlCommand("CreateCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command'
                        command.Parameters.AddWithValue("@KursusNavn", courseName);

                        SqlParameter teacherIdParam = new SqlParameter("@UnderviserId", SqlDbType.Int);
                        teacherIdParam.Value = (object)courseTeacerId ?? DBNull.Value;
                        command.Parameters.Add(teacherIdParam);

                        SqlParameter roomIdParam = new SqlParameter("@LokaleId", SqlDbType.Int);
                        roomIdParam.Value = (object)courseRoomId ?? DBNull.Value;
                        command.Parameters.Add(roomIdParam);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

 


        public void createTeacher(string firstName, string lastName, string username,string password)
        {
            string encodedPassword = HttpUtility.UrlEncode(password);
            try
            {

                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();


                    // Create a SqlCommand object for calling the stored procedure
                    using (SqlCommand command = new SqlCommand("CreateTeacher", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@BrugerNavn", lastName);
                        command.Parameters.AddWithValue("@Password", encodedPassword);


                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }


        public void createAdmin(string username, string password)
        {
            try
            {
                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand object for calling the stored procedure
                    using (SqlCommand command = new SqlCommand("CreateAdmin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }



        public class Course
        {
            public int CourseId { get; set; }
            public string CourseName { get; set; }
        }

        public List<Course> getCourses()
        {
            List<Course> courses = new List<Course>();

            try
            {
                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand object for executing the query
                    string sqlQuery = "SELECT KursusId, KursusNavn FROM GetAllCourses"; // Using the view instead of the table directly
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Iterate through the rows and add them to the list
                                while (reader.Read())
                                {
                                    Course course = new Course();
                                    course.CourseId = reader.GetInt32(0); // Assuming KursusId is at index 0
                                    course.CourseName = reader.GetString(1); // Assuming KursusNavn is at index 1
                                    courses.Add(course);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return courses;
        }





        public class Teacher
        {
            public int TeacherId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public List<Teacher> getTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();

            try
            {
                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand object for executing the query
                    string sqlQuery = "SELECT TeacherId, FirstName, LastName FROM GetAllTeachers";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Iterate through the rows and add them to the list
                                while (reader.Read())
                                {
                                    Teacher teacher = new Teacher();
                                    teacher.TeacherId = reader.GetInt32(0); // Assuming TeacherId is at index 0
                                    teacher.FirstName = reader.GetString(1); // Assuming FirstName is at index 1
                                    teacher.LastName = reader.GetString(2); // Assuming LastName is at index 2
                                    teachers.Add(teacher);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return teachers;
        }



        public string getData(string view, string query)
            {
                try
                {
                    var builder = msSqlConnection();

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {
                        connection.Open();

                        // Create a SqlCommand object for executing the query
                        string sqlQuery = $"SELECT {query} FROM {view}";
                        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Check if there are any rows returned
                                if (reader.HasRows)
                                {
                                    // Create a dictionary to store column names and values
                                    var result = new List<Dictionary<string, object>>();

                                    // Iterate through the rows and add them to the list
                                    while (reader.Read())
                                    {
                                        var dict = new Dictionary<string, object>();
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            dict[reader.GetName(i)] = reader.GetValue(i);
                                        }
                                        result.Add(dict);
                                    }

                                    // Serialize the result list to JSON
                                    return JsonConvert.SerializeObject(result);
                                }
                                else
                                {
                                    Console.WriteLine("No rows found.");
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return null;
            }





    public class Room
        {
            public int RoomId { get; set; }
            public string RoomName { get; set; }
        }
        public List<Room> getRooms()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    // Create a SqlCommand object for executing the query
                    string sqlQuery = "SELECT LokaleId, LokaleNavn FROM GetAllRooms";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any rows returned
                            if (reader.HasRows)
                            {
                                // Iterate through the rows and add them to the list
                                while (reader.Read())
                                {
                                    Room room = new Room();
                                    room.RoomId = reader.GetInt32(0); // Assuming LokaleId is at index 0
                                    room.RoomName = reader.GetString(1); // Assuming LokaleNavn is at index 1
                                    rooms.Add(room);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No rows found.");
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return rooms;
        }

        public bool AdminLogin(string username, string password)
        {
            bool isValid = false;
            Debug.WriteLine("THIS IS THE DATAAHANDLER");
            try
            {
                var builder = msSqlConnection();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CheckAdminCredentials", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        SqlParameter returnParameter = command.Parameters.Add("@IsValid", SqlDbType.Bit);
                        returnParameter.Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();

                       
                        isValid = (bool)returnParameter.Value;
                        Debug.WriteLine($"result: {isValid}");
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return isValid;
        }
    }




}













































