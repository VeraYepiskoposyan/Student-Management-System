using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentManagementSystem
{
    internal class StudentScore
    {
        public string StudentName { get; set; }
        public string Subject { get; set; }
        public string Score { get; set; }
        public string ScoreType { get; set; }
        public StudentScore(string studentName, string subject, string score, string type)
        {
            StudentName = studentName;
            Subject = subject;
            Score = score;
            ScoreType = type;
        }
    }
}
