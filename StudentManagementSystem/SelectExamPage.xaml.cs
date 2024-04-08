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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StudentManagementSystem.Pages
{
    public partial class SetExamScoresPage : Page
    {
        private List<Exam> exams;
        private List<Student> students;

        public SetExamScoresPage(List<Exam> exams, List<Student> students)
        {
            InitializeComponent();
            this.exams = exams;
            this.students = students;
            LoadExams();
            LoadStudents();
        }

        private void LoadExams()
        {
            ExamsListBox.ItemsSource = exams;
            ExamsListBox.DisplayMemberPath = "Description"; // Display exam descriptions in the list
        }

        private void LoadStudents()
        {
            StudentsListBox.ItemsSource = students;
            StudentsListBox.DisplayMemberPath = "FullName"; // Display student full names in the list
        }

        private void SaveScore_Click(object sender, RoutedEventArgs e)
        {
            if (ExamsListBox.SelectedItem != null && StudentsListBox.SelectedItem != null)
            {
                Exam selectedExam = (Exam)ExamsListBox.SelectedItem;
                Student selectedStudent = (Student)StudentsListBox.SelectedItem;

                // Extract score from the TextBox or other input control
                if (double.TryParse(ScoreTextBox.Text, out double score))
                {
                    // Save the score for the selected student in the selected exam
                    // Implement logic to update the database with the student's score for the exam

                    MessageBox.Show($"Score saved successfully for {selectedStudent.FullName} in {selectedExam.Description}");
                }
                else
                {
                    MessageBox.Show("Please enter a valid score.");
                }
            }
            else
            {
                MessageBox.Show("Please select an exam and a student.");
            }
        }
    }
}