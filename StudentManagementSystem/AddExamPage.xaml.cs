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
    public partial class AddExamPage : Page
    {
        public event EventHandler<Exam> ExamAdded;

        public AddExamPage()
        {
            InitializeComponent();
        }

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
            string description = DescriptionTextBox.Text;
            string place = PlaceTextBox.Text;
            int seats = int.Parse(SeatsTextBox.Text);
            DateTime date = ExamDatePicker.SelectedDate ?? DateTime.Today; // Get selected date or default to today's date

            Exam exam = new     Exam { Description = description, Place = place, Seats = seats, Date = date };

            ExamAdded?.Invoke(this, exam);
        }

        private void SeatsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure only numeric input for seats
            if (!int.TryParse(SeatsTextBox.Text, out _))
            {
                MessageBox.Show("Please enter a valid number for seats.");
                SeatsTextBox.Text = "";
            }
        }
    }
}