using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagementSystem.Pages
{
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool isProfessor = ProfessorRadioButton.IsChecked == true;

            string cardId = IDTextBox.Text;
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string hashedPassword = HashPassword(password);


            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Users (CardID, Name, Surname, Email, Password, Role) VALUES (@CardID, @Name, @Surname, @Email, @Password, @Role)"; 

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@CardID", cardId);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Surname", surname);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);
                        cmd.Parameters.AddWithValue("@Role", isProfessor ? "Professor" : "Student");


                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) inserted.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void ProfessorRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SubjectLabel.Visibility = Visibility.Visible;
            SubjectTextBox.Visibility = Visibility.Visible;
        }

        private void StudentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SubjectLabel.Visibility = Visibility.Collapsed;
            SubjectTextBox.Visibility = Visibility.Collapsed;
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
