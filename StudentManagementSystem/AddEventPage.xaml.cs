using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.SqlClient;

namespace StudentManagementSystem.Pages
{
    public partial class AddEventPage : Page
    {
        public event EventHandler<Event> EventAdded;

        public AddEventPage()
        {
            InitializeComponent();
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            DateTime eventDate = EventDatePicker.SelectedDate ?? DateTime.Now;
            string description = DescriptionTextBox.Text;

            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            string sql = "INSERT INTO Events (Date, Description) VALUES (@Date, @Description)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        cmd.Parameters.AddWithValue("@Date", eventDate.ToString());
                        cmd.Parameters.AddWithValue("@Description", description);

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
    }
}