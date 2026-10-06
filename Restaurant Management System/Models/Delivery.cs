using Restaurant_Management_System.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime? DeliveryTime { get; set; }
        public DeliveryStatus Status { get; set; } = DeliveryStatus.AwaitingAssignment;
        public int? DeliveryStaffId { get; set; }
        public string? FailureReason { get; set; }
    }
}
