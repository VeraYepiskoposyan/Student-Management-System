using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace StudentManagementSystem
{
    /// <summary>
    /// Interaction logic for ViewEvent.xaml
    /// </summary>
    public partial class ViewEvent : Page
    {
        private List<Event> Events { get; set; }
        public void InitEventsList()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Events";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                           
                            string date = reader["Date"].ToString();
                            string descr = reader["Description"].ToString();

                            Events.Add(new Event(date, descr));
                        }
                        EventDataGrid.ItemsSource = Events;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }
        public ViewEvent()
        {
            InitializeComponent();
            Events = new List<Event>();
            InitEventsList();
        }
    }
}
