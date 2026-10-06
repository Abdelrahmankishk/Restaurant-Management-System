using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class DeliveryStaff
    {
        public int StaffId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty; 
        public string LicenseNumber { get; set; } = string.Empty;
        public string AssignedArea { get; set; } = string.Empty; 
        public int BranchId { get; set; }
        public bool IsAvailable { get; set; }

    }
}
