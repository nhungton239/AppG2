using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppG2.Model
{
    public class Student
    {
        
        public string IDStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GENDER Gender { get; set; }
        public DateTime DOB { get; set; } //Date Of Birth
        public string POB { get; set; } // Place Of Birth
        public ICollection<HistoryLearning> ListHistoryLearning { get; set; } // thiet lap quan he vs History Learning

       
    }
    public enum GENDER
    {
        Male, Female, Other
        // Male = 0; Female = 1; Other = 2;
    }
}
