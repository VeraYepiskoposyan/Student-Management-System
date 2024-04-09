using StudentManagementSystem.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    public class Event
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public Event(string date, string descr)
        {
            Date = date;
            Description = descr;
        }
    }
}
