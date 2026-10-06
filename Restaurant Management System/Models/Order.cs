using Restaurant_Management_System.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime DateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int BranchId { get; set; }
        public int HandledByEmployeeId { get; set; }
        public int CustomerId { get; set; }
        public string? Address { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
