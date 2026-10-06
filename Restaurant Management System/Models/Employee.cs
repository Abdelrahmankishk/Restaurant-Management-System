using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public abstract class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty; 
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime DateOfHire { get; set; }
        public string ContactInfo { get; set; } = string.Empty;
        public int PrimaryBranchId { get; set; }
        public List<int> AssignedBranchIds { get; set; } = new();
        public abstract string GetRole();
    }
}
