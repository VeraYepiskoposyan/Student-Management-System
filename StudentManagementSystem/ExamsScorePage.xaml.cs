using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace StudentManagementSystem.Pages
{
    public partial class ExamsScorePage : Page
    {
        private int ProfessorID { get; set; }
        private List<Student> Students { get; set; }
        private List<Exam> Exams { get; set; }


        public ExamsScorePage(int professorID)
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
        private void AddScore_Click(object sender, RoutedEventArgs e)
        {
            bool isExam = ExamRadioButton.IsChecked == true;
            string scoreType = isExam ? "Exam" : "Task";
            string score = ScoreTextBox.Text;
            int StudentId = 0;
            string subject = "";
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
            string sql = "INSERT INTO StudentScoreInfo (StudentID, Score, Subject, ProfessorID, Type) VALUES (@StudentID, @Score, @Subject, @ProfessorID, @Type)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        cmd.Parameters.AddWithValue("@StudentID", StudentId);
                        cmd.Parameters.AddWithValue("@Score", score);
                        cmd.Parameters.AddWithValue("@ProfessorID", ProfessorID);
                        cmd.Parameters.AddWithValue("@Subject", subject);
                        cmd.Parameters.AddWithValue("@Type", scoreType);


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

