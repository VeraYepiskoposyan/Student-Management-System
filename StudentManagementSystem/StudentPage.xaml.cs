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

        public StudentPage(int id, string cardId, string name, string surname)
        {
            InitializeComponent();
            StudentID = id;
            CardID = cardId;
            StudentName = name;
            StudentSurname = surname;
            LoadStudentsFromDatabase();
        }

        private void LoadStudentsFromDatabase()
        {
            Students = new ObservableCollection<Student>();

            string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=YourDatabaseName;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ExamScore WHERE StudentID = @StudentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", StudentID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            int score = Convert.ToInt32(reader["Score"]);
                            string subjrct = reader["Name"].ToString();
                            string surname = reader["Surname"].ToString();
                            double examScore = Convert.ToDouble(reader["ExamScore"]);
                            double taskScore = Convert.ToDouble(reader["TaskScore"]);

                            //Students.Add(new Student { ID = id, Name = name, Surname = surname, ExamScore = examScore, TaskScore = taskScore });
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

            DataContext = this;
        }
    }
}