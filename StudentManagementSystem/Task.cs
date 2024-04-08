using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    public class Task
    {
        public int StudentId { get; set; }
        public DateTime Deadline { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
