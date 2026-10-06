using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class Chef : Employee
    {
        public Chef() { 
            Position = "Chef";
        }
        public override string GetRole() => "Chef";
    }
}
