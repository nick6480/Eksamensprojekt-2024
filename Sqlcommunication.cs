using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace HttpListenerExample
{
    class SQLCommunication
    {
        public static async Task<byte[]> GetDataFromSQLServer()
        {
            // Opret forbindelse til databasen
            string connectionString = "Data Source=myServerAddress;Initial Catalog=dbo;User ID=sa;Password=dockerStrongPwd123;";


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Forespørgsel til dbo.Person
                    string personQuery = "SELECT * FROM dbo.Person";
                    StringBuilder personData = new StringBuilder();
                    using (SqlCommand personCommand = new SqlCommand(personQuery, connection))
                    {
                        using (SqlDataReader personReader = await personCommand.ExecuteReaderAsync())
                        {
                            while (personReader.Read())
                            {
                                personData.Append($"PersonId: {personReader["PersonId"]}, Fornavn: {personReader["Fornavn"]}, Mellemnavn: {personReader["Mellemnavn"]}, Efternavn: {personReader["Efternavn"]}\n");
                            }
                        }
                    }

                    // Forespørgsel til dbo.Login
                    string loginQuery = "SELECT * FROM dbo.Login";
                    StringBuilder loginData = new StringBuilder();
                    using (SqlCommand loginCommand = new SqlCommand(loginQuery, connection))
                    {
                        using (SqlDataReader loginReader = await loginCommand.ExecuteReaderAsync())
                        {
                            while (loginReader.Read())
                            {
                                loginData.Append($"Brugernavn: {loginReader["Brugernavn"]}, PasswordHash: {loginReader["PasswordHash"]}\n");
                            }
                        }
                    }

                    // Forespørgsel til dbo.Mail
                    string mailQuery = "SELECT * FROM dbo.Mail";
                    StringBuilder mailData = new StringBuilder();
                    using (SqlCommand mailCommand = new SqlCommand(mailQuery, connection))
                    {
                        using (SqlDataReader mailReader = await mailCommand.ExecuteReaderAsync())
                        {
                            while (mailReader.Read())
                            {
                                mailData.Append($"MailAdresseId: {mailReader["MailAdresseId"]}, StudentId: {mailReader["StudentId"]}\n");
                            }
                        }
                    }

                    // Forespørgsel til dbo.Student
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

                    // Forespørgsel til dbo.Underviser
                    string underviserQuery = "SELECT * FROM dbo.Underviser";
                    StringBuilder underviserData = new StringBuilder();
                    using (SqlCommand underviserCommand = new SqlCommand(underviserQuery, connection))
                    {
                        using (SqlDataReader underviserReader = await underviserCommand.ExecuteReaderAsync())
                        {
                            while (underviserReader.Read())
                            {
                                underviserData.Append($"UnderviserId: {underviserReader["UnderviserId"]}, PersonId: {underviserReader["PersonId"]}\n");
                            }
                        }
                    }

                    // Forespørgsel til dbo.Ansatte
                    string ansatteQuery = "SELECT * FROM dbo.Ansatte";
                    StringBuilder ansatteData = new StringBuilder();
                    using (SqlCommand ansatteCommand = new SqlCommand(ansatteQuery, connection))
                    {
                        using (SqlDataReader ansatteReader = await ansatteCommand.ExecuteReaderAsync())
                        {
                            while (ansatteReader.Read())
                            {
                                ansatteData.Append($"MedarbejderNr: {ansatteReader["MedarbejderNr"]}, PersonId: {ansatteReader["PersonId"]}\n");
                            }
                        }
                    }

                    // Forespørgsel til dbo.Lokaler
                    string lokaleQuery = "SELECT * FROM dbo.Lokaler";
                    StringBuilder lokaleData = new StringBuilder();
                    using (SqlCommand lokaleCommand = new SqlCommand(lokaleQuery, connection))
                    {
                        using (SqlDataReader lokaleReader = await lokaleCommand.ExecuteReaderAsync())
                        {
                            while (lokaleReader.Read())
                            {
                                lokaleData.Append($"LokaleId: {lokaleReader["LokaleId"]}, Lokalenum: {lokaleReader["Lokalenum"]}, KursusId: {lokaleReader["KursusId"]}\n");
                            }
                        }
                    }

                    // Forespørgsel til dbo.Kursus
                    string kursusQuery = "SELECT * FROM dbo.Kursus";
                    StringBuilder kursusData = new StringBuilder();
                    using (SqlCommand kursusCommand = new SqlCommand(kursusQuery, connection))
                    {
                        using (SqlDataReader kursusReader = await kursusCommand.ExecuteReaderAsync())
                        {
                            while (kursusReader.Read())
                            {
                                kursusData.Append($"KursusId: {kursusReader["KursusId"]}, Underviserid: {kursusReader["Underviserid"]}, KursusNavn: {kursusReader["KursusNavn"]}\n");
                            }
                        }
                    }

                    // Saml data fra alle forespørgsler
                    StringBuilder jsonData = new StringBuilder();
                    jsonData.AppendLine("Person data:");
                    jsonData.AppendLine(personData.ToString());
                    jsonData.AppendLine("Login data:");
                    jsonData.AppendLine(loginData.ToString());
                    jsonData.AppendLine("Mail data:");
                    jsonData.AppendLine(mailData.ToString());
                    jsonData.AppendLine("Student data:");
                    jsonData.AppendLine(studentData.ToString());
                    jsonData.AppendLine("Underviser data:");
                    jsonData.AppendLine(underviserData.ToString());
                    jsonData.AppendLine("Ansatte data:");
                    jsonData.AppendLine(ansatteData.ToString());
                    jsonData.AppendLine("Lokaler data:");
                    jsonData.AppendLine(lokaleData.ToString());
                    jsonData.AppendLine("Kursus data:");
                    jsonData.AppendLine(kursusData.ToString());

                    // Konverter data til byte array og returner
                    byte[] responseData = Encoding.UTF8.GetBytes(jsonData.ToString());
                    return responseData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
                return null;
            }
        }
    }
}
 
