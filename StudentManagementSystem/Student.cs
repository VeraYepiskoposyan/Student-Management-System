using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem
{
    public class Student
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName => $"{Name} {Surname}";
 
        public Student(int id, string name, string surName)
        {
            ID = id;
            Name = name;
            Surname = surName;
        }
    }
}
