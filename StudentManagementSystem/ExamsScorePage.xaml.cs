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

namespace StudentManagementSystem.Pages
{
    public partial class ExamsScorePage : Page
    {
        public ExamsScorePage()
        {
            InitializeComponent();

            LoadExamScores();
        }

        private void LoadExamScores()
        {
            ObservableCollection<ExamScore> examScores = new ObservableCollection<ExamScore>
            {
                new ExamScore { StudentName = "John Doe", Subject = "Math", Score = 90 },
                new ExamScore { StudentName = "Jane Smith", Subject = "Science", Score = 85 },
                new ExamScore { StudentName = "Alice Johnson", Subject = "History", Score = 95 }
            };

            ExamsScoreDataGrid.ItemsSource = examScores;
        }

        public class ExamScore
        {
            public string StudentName { get; set; }
            public string Subject { get; set; }
            public double Score { get; set; }
        }
    }
}
