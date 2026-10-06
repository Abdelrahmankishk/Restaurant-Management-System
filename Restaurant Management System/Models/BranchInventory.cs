using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Models
{
    public class BranchInventory
    {
        public int BranchId { get; set; }
        public int IngredientId { get; set; }
        public double CurrentQuantity { get; set; }
    }
}
