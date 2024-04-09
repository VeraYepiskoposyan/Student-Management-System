using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    public class Exam
    {
        // Properties
        public string Subject { get; set; }
        public int Place { get; set; }
        public int Seats { get; set; }
        public string Date { get; set; }

        // Constructor
        public Exam(string subject, int place, int seats, string date)
        {
            Subject = subject;
            Place = place;
            Seats = seats;
            Date = date;
        }
    }
}
