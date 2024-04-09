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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentManagementSystem.Pages
{
    public partial class AddTaskPage : Page
    {
        public event EventHandler<Task> TaskAdded;
        private int ProfessorID { get; set; }
        private List<Student> Students { get; set; }
        private List<Exam> Exams { get; set; }

        public AddTaskPage(int professorID)
        {
            InitializeComponent();
            Students = new List<Student>();
            Exams = new List<Exam>();
            ProfessorID = professorID;
            InitStudentList();
            InitExamList();
        }
        public void InitStudentList()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM Users WHERE Role = @Role";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@Role", "Student");

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int Id = Convert.ToInt32(reader["ID"].ToString());
                            string name = reader["Name"].ToString();
                            string surname = reader["Surname"].ToString();
                            Students.Add(new Student(Id, name, surname));
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            StudentComboBox.ItemsSource = Students;
        }
        public void InitExamList()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM ExamInfo WHERE ProfessorID = @ProfessorID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@ProfessorID", ProfessorID);

                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int seats = Convert.ToInt32(reader["Seatnumber"].ToString());
                            int place = Convert.ToInt32(reader["Place"].ToString());
                            string date = reader["Date"].ToString();
                            string subject = reader["Subject"].ToString();
                            Exams.Add(new Exam(subject, place, seats, date));
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            ExamComboBox.ItemsSource = Exams;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            DateTime deadline = DeadlineDatePicker.SelectedDate ?? DateTime.Now;
            string subject = "";
            string description = DescriptionTextBox.Text;
            int StudentId = 0;
            if (ExamComboBox.SelectedItem != null)
            {
                // Get the selected student object
                Exam exam = (Exam)ExamComboBox.SelectedItem;
                subject = exam.Subject;
            }
            if (StudentComboBox.SelectedItem != null)
            {
                // Get the selected student object
                Student student = (Student)StudentComboBox.SelectedItem;
                StudentId = student.ID;
            }

            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            string sql = "INSERT INTO ExamInfo (StudentID, Deadline, Description, Subject) VALUES (@StudentID, @Deadline, @Description, @Subject)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        cmd.Parameters.AddWithValue("@StudentID", StudentId);
                        cmd.Parameters.AddWithValue("@Deadline", deadline.ToString());
                        cmd.Parameters.AddWithValue("@Description", description);
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
