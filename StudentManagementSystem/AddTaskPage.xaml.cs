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
    public partial class AddTaskPage : Page
    {
        public event EventHandler<Task> TaskAdded;

        public AddTaskPage()
        {
            InitializeComponent();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            int studentId = int.Parse(StudentIdTextBox.Text);
            DateTime deadline = DeadlineDatePicker.SelectedDate ?? DateTime.Now;
            string subject = SubjectTextBox.Text;
            string description = DescriptionTextBox.Text;

            Task newTask = new Task { StudentId = studentId, Deadline = deadline, Subject = subject, Description = description };

            TaskAdded?.Invoke(this, newTask);
        }
    }
}
