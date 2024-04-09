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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace StudentManagementSystem.Pages
{
    public partial class StudentPage : Page
    {
        public ObservableCollection<Student> Students { get; set; }

        public int StudentID { get; set; }
        public string CardID { get; set; }
        public string StudentName { get; set; }
        public string StudentSurname { get; set; }

        private List<StudentScore> StudentScores { get; set; }

        public StudentPage(int id, string cardId, string name, string surname)
        {
            InitializeComponent();
            StudentID = id;
            CardID = cardId;
            StudentName = name;
            StudentSurname = surname;
            LoadScoresFromDatabase();
        }

        private void LoadScoresFromDatabase()
        {
            StudentScores = new List<StudentScore>();
            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=StudentSystem;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT * FROM StudentScoreInfo WHERE StudentID = @StudentID";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@StudentID", StudentID);
                        connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string score = reader["Score"].ToString();
                            string subject = reader["Subject"].ToString();
                            string type = reader["Type"].ToString();
                            StudentScores.Add(new StudentScore(StudentName, subject, score, type));
                        }
                        ScoreDataGrid.ItemsSource = StudentScores;
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
            DataContext = this;
        }
    }
}