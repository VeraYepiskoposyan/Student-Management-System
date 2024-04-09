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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentManagementSystem.Pages
{
    public partial class AddExamPage : Page
    {
        public event EventHandler<Exam> ExamAdded;

        private int ProfessorID { get; set; }
        public AddExamPage(int professorId)
        {
            InitializeComponent();
            ProfessorID = professorId;
        }

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
           
            int place = int.Parse(PlaceTextBox.Text);
            int seats = int.Parse(SeatsTextBox.Text);
            string subject = SubjectTextBox.Text;
            DateTime date = ExamDatePicker.SelectedDate ?? DateTime.Today; // Get selected date or default to today's date
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO ExamInfo (ProfessorID, Place, Seatnumber, Date, Subject) VALUES (@ProfessorID, @Place, @Seatnumber, @Date, @Subject)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        
                        cmd.Parameters.AddWithValue("@ProfessorID", ProfessorID);
                        cmd.Parameters.AddWithValue("@Place", place);
                        cmd.Parameters.AddWithValue("@Seatnumber", seats);
                        cmd.Parameters.AddWithValue("@Date", date.ToString());
                        cmd.Parameters.AddWithValue("@Subject", subject);


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