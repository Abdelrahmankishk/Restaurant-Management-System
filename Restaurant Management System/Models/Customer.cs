using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int LoyaltyPoints { get; set; }
        public string? Email { get; set; }
    }
}
