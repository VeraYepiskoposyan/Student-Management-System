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
        public string Description { get; set; }
        public string Place { get; set; }
        public int Seats { get; set; }
        public DateTime Date { get; set; }

        // Constructor
        public Exam(string description, string place, int seats, DateTime date)
        {
            Description = description;
            Place = place;
            Seats = seats;
            Date = date;
        }
    }
}
