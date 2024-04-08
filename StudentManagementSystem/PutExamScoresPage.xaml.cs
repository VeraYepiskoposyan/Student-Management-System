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

namespace StudentManagementSystem.Pages
{
    public partial class PutExamScoresPage : Page
    {
        public PutExamScoresPage()
        {
            InitializeComponent();
        }

        private void PutExamScore_Click(object sender, RoutedEventArgs e)
        {
            // Extract input data
            int studentId = int.Parse(StudentIdTextBox.Text);
            string subject = SubjectTextBox.Text;
            double score = double.Parse(ScoreTextBox.Text);

            // Create an Exam object
            Exam exam = new Exam { StudentId = studentId, Subject = subject, Score = score };

            // TODO: Add logic to update exam score in the database

            // Display success message
            MessageBox.Show($"Exam score for student with ID {studentId} in {subject} updated successfully.");
        }
    }
}