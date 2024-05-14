using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using HttpListenerExample;

namespace HttpListenerExample
{
    // Class responsible for communication with SQL Server
    class SQLCommunication
    {
        // Method to asynchronously retrieve data from SQL Server
        public static async Task<byte[]> GetDataFromSQLServer()
        {
            // Establish connection to the database
            string connectionString = "Data Source=myServerAddress;Initial Catalog=dbo;User ID=sa;Password=dockerStrongPwd123;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Query to dbo.Person table
                    string personQuery = "SELECT * FROM dbo.Person";
                    StringBuilder personData = new StringBuilder();
                    using (SqlCommand personCommand = new SqlCommand(personQuery, connection))
                    {
                        using (SqlDataReader personReader = await personCommand.ExecuteReaderAsync())
                        {
                            while (personReader.Read())
                            {
                                personData.Append($"PersonId: {personReader["PersonId"]}, Firstname: {personReader["Firstname"]}, Middlename: {personReader["Middlename"]}, Lastname: {personReader["Lastname"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Login table
                    string loginQuery = "SELECT * FROM dbo.Login";
                    StringBuilder loginData = new StringBuilder();
                    using (SqlCommand loginCommand = new SqlCommand(loginQuery, connection))
                    {
                        using (SqlDataReader loginReader = await loginCommand.ExecuteReaderAsync())
                        {
                            while (loginReader.Read())
                            {
                                loginData.Append($"Username: {loginReader["Username"]}, PasswordHash: {loginReader["PasswordHash"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Mail table
                    string mailQuery = "SELECT * FROM dbo.Mail";
                    StringBuilder mailData = new StringBuilder();
                    using (SqlCommand mailCommand = new SqlCommand(mailQuery, connection))
                    {
                        using (SqlDataReader mailReader = await mailCommand.ExecuteReaderAsync())
                        {
                            while (mailReader.Read())
                            {
                                mailData.Append($"MailAddressId: {mailReader["MailAddressId"]}, StudentId: {mailReader["StudentId"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Student table
                    string studentQuery = "SELECT * FROM dbo.Student";
                    StringBuilder studentData = new StringBuilder();
                    using (SqlCommand studentCommand = new SqlCommand(studentQuery, connection))
                    {
                        using (SqlDataReader studentReader = await studentCommand.ExecuteReaderAsync())
                        {
                            while (studentReader.Read())
                            {
                                studentData.Append($"StudentId: {studentReader["StudentId"]}, PersonId: {studentReader["PersonId"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Teacher table
                    string teacherQuery = "SELECT * FROM dbo.Teacher";
                    StringBuilder teacherData = new StringBuilder();
                    using (SqlCommand teacherCommand = new SqlCommand(teacherQuery, connection))
                    {
                        using (SqlDataReader teacherReader = await teacherCommand.ExecuteReaderAsync())
                        {
                            while (teacherReader.Read())
                            {
                                teacherData.Append($"TeacherId: {teacherReader["TeacherId"]}, PersonId: {teacherReader["PersonId"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Employee table
                    string employeeQuery = "SELECT * FROM dbo.Employee";
                    StringBuilder employeeData = new StringBuilder();
                    using (SqlCommand employeeCommand = new SqlCommand(employeeQuery, connection))
                    {
                        using (SqlDataReader employeeReader = await employeeCommand.ExecuteReaderAsync())
                        {
                            while (employeeReader.Read())
                            {
                                employeeData.Append($"EmployeeNo: {employeeReader["EmployeeNo"]}, PersonId: {employeeReader["PersonId"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Room table
                    string roomQuery = "SELECT * FROM dbo.Room";
                    StringBuilder roomData = new StringBuilder();
                    using (SqlCommand roomCommand = new SqlCommand(roomQuery, connection))
                    {
                        using (SqlDataReader roomReader = await roomCommand.ExecuteReaderAsync())
                        {
                            while (roomReader.Read())
                            {
                                roomData.Append($"RoomId: {roomReader["RoomId"]}, RoomNo: {roomReader["RoomNo"]}, CourseId: {roomReader["CourseId"]}\n");
                            }
                        }
                    }

                    // Query to dbo.Course table
                    string courseQuery = "SELECT * FROM dbo.Course";
                    StringBuilder courseData = new StringBuilder();
                    using (SqlCommand courseCommand = new SqlCommand(courseQuery, connection))
                    {
                        using (SqlDataReader courseReader = await courseCommand.ExecuteReaderAsync())
                        {
                            while (courseReader.Read())
                            {
                                courseData.Append($"CourseId: {courseReader["CourseId"]}, TeacherId: {courseReader["TeacherId"]}, CourseName: {courseReader["CourseName"]}\n");
                            }
                        }
                    }

                    // Assemble data from all queries
                    StringBuilder jsonData = new StringBuilder();
                    jsonData.AppendLine("Person data:");
                    jsonData.AppendLine(personData.ToString());
                    jsonData.AppendLine("Login data:");
                    jsonData.AppendLine(loginData.ToString());
                    jsonData.AppendLine("Mail data:");
                    jsonData.AppendLine(mailData.ToString());
                    jsonData.AppendLine("Student data:");
                    jsonData.AppendLine(studentData.ToString());
                    jsonData.AppendLine("Teacher data:");
                    jsonData.AppendLine(teacherData.ToString());
                    jsonData.AppendLine("Employee data:");
                    jsonData.AppendLine(employeeData.ToString());
                    jsonData.AppendLine("Room data:");
                    jsonData.AppendLine(roomData.ToString());
                    jsonData.AppendLine("Course data:");
                    jsonData.AppendLine(courseData.ToString());

                    // Convert data to byte array and return
                    byte[] responseData = Encoding.UTF8.GetBytes(jsonData.ToString());
                    return responseData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}

 

