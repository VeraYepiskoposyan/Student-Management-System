using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for ViewExamScores.xaml
    /// </summary>
    public partial class ViewExamScores : Page
    {
        private List<Student> Students { get; set; }
        private List<StudentScore> StudentScores { get; set; }
        string GetNameById(int id)
        {
            // LINQ query to find the student with the matching ID
            Student student = Students.FirstOrDefault(s => s.ID == id);
            return student != null ? student.FullName : "Unknown";
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
        }
        public void InitStudentScoreList()
        {
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM StudentScoreInfo";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string score = reader["Score"].ToString();
                            string name = GetNameById(Convert.ToInt32(reader["StudentID"].ToString()));
                            string subject = reader["Subject"].ToString();
                            string type = reader["Type"].ToString();
                            StudentScores.Add(new StudentScore(name, subject, score, type));
                        }
                        ExamScoreDataGrid.ItemsSource = StudentScores;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }
        public ViewExamScores()
        {
            InitializeComponent();
            Students = new List<Student>();
            StudentScores = new List<StudentScore>();
            InitStudentList();
            InitStudentScoreList();
        }
    }
}
