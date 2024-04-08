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
    public partial class ProfessorPage : Page
    {
        public int ProfessorID { get; set; }
        public string CardID { get; set; }
        public string ProfessorName { get; set; }
        public string ProfessorSurname { get; set; }

        public ProfessorPage(int professorID, string cardId, string professorName, string professorSurname)
        {
            InitializeComponent();

            ProfessorID = professorID;
            CardID = cardId;
            ProfessorName = professorName;
            ProfessorSurname = professorSurname;

            DataContext = this;
        }

        private void AddExam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddExamPage());
        }

        private void PutExamScores_Click(object sender, RoutedEventArgs e)
        {
            // First, navigate to a page where the professor selects the exam
            NavigationService?.Navigate(new SelectExamPage(ProfessorID));
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddEventPage());
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddTaskPage());
        }
    }
}