using System;
using System.Windows;
using System.Data.SqlClient;
using StudentManagementSystem.Pages;
using System.Windows.Controls;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;


namespace StudentManagementSystem.Pages
{
    public partial class LogInPage : Page
    {
        public LogInPage()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string hashedPassword = HashPassword(password);

            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", hashedPassword);
                

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    
                    if (reader.HasRows)
                    {
                        reader.Read();

                        int Id =  Convert.ToInt32(reader["ID"].ToString());
                        string cardId = reader["CardID"].ToString();
                        string name = reader["Name"].ToString();
                        string surname = reader["Surname"].ToString();
                        string role = reader["Role"].ToString();
                        MessageBox.Show(role);
                        if (role.Equals("Student"))
                        {

                            MessageBox.Show("Login successful as student.");
                            NavigationService?.Navigate(new StudentPage(Id, cardId, name, surname));
                        }
                        else
                        {
                            MessageBox.Show("Login successful as Professor.");
                            NavigationService?.Navigate(new ProfessorPage(Id, cardId, name, surname));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials.");
                    }

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }
        private string HashPassword(string password)
        {
            string source = "Ajay Vishwakarma!";
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;
            }

        }
    }
}
