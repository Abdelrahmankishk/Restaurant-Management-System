using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class Feedback
    {
        public int FeedBackId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
