using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class MenuItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<AddOn> AddOns { get; set; } = new();
    }
}
