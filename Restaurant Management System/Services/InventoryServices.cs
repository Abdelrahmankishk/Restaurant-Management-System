using Restaurant_Management_System.Helpers;
using Restaurant_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Services
{
    public static class InventoryServices
    {
        public static Dictionary<int,double> GetShortfalls(int branchId,List<OrderItem> items)
        {
            var requiredQuantities = new Dictionary<int, double>();
            foreach(var item in items)
            {
                var recipeItems = DataSeeder.RecipeItems.Where(ri => ri.MenuItemId == item.ItemId);
                foreach(var recipeItem in recipeItems)
                {
                    if(!requiredQuantities.ContainsKey(recipeItem.IngredientId))
                    {
                        requiredQuantities[recipeItem.IngredientId] = 0;
                    }
                    requiredQuantities[recipeItem.IngredientId] += recipeItem.QuantityRequired * item.Quantity;
                }
            }
            var shortfalls = new Dictionary<int, double>();
            foreach(var (ingredientId, Quantity) in requiredQuantities)
            {
                var inventoryItem = DataSeeder.BranchInventories.FirstOrDefault(x => x.BranchId == branchId && x.IngredientId == ingredientId);
                double availableQuantity = inventoryItem?.CurrentQuantity ?? 0;
                if(availableQuantity < Quantity)
                    shortfalls[ingredientId] = Quantity - availableQuantity;
            }
            return shortfalls;
        }
    }
}
