using Restaurant_Management_System.Helpers;
using Restaurant_Management_System.Models;
using Restaurant_Management_System.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Services
{
    public static class OrderServices
    {
        public static (bool Success , string Message) PlaceOrder(int employeeId, int CustomerId,int BranchId,OrderType orderType,string? deliveryAddress, List<(int ItemId, int Qty, string? notes, List<int> AddOnIds)> items)
        {
            var employee = DataSeeder.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            //1.Only Waiters and Cashiers can place orders
            if (employee is null)
                return (false, "Employee not found");
            if (employee is not Waiter && employee is not Cashier)
                return (false, "Only Waiters and Cashiers can place orders");

            //2.Employee must belong to same branch as order
            if (!employee.AssignedBranchIds.Contains(BranchId))
                return (false, "Employee does not belong to the specified branch");

            var customer = DataSeeder.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
            if (customer is null)
                return (false, "Customer not found");

            var branch = DataSeeder.Branches.FirstOrDefault(b => b.BranchId == BranchId);
            if (branch is null)
                return (false, "Branch not found");

            //3.Delivery orders require delivery address
            if (orderType == OrderType.Delivery && string.IsNullOrWhiteSpace(deliveryAddress))
                return (false, "Delivery orders require a delivery address");
            // 4.Order must have at least one item
            if (items is null || items.Count == 0)
                return (false, "Order must have at least one item");
            //Build OrderItem
            var orderItems = new List<OrderItem>();
            int itemSeq = DataSeeder.NextOrderItemId;
            foreach (var (ItemId, Qty, notes, AddOnIds) in items)
            {
                //5.Item quantity must be greater than zero
                if (Qty <= 0)
                    return (false, $"Item quantity must be greater than zero for ItemId {ItemId}");
                //6. Menu item must be available at branch
                var branchItem = DataSeeder.BranchMenuItems.FirstOrDefault(bi => bi.BranchId == BranchId && bi.ItemId == ItemId);
                if (branchItem is null || !branchItem.IsAvailable)
                    return (false, $"ItemId {ItemId} is not available at the specified branch");

                var menuItem = DataSeeder.MenuItems.FirstOrDefault(mi => mi.ItemId == ItemId);
                if (menuItem is null)
                    return (false, $"ItemId {ItemId} not found in menu");

                //7.Use branch-specific price if set branch price override
                decimal unitPrice = branchItem.PriceOverride ?? menuItem.BasePrice;
                //Add ON Price Override
                foreach (var addOnId in AddOnIds)
                {
                    var addOn = menuItem.AddOns.FirstOrDefault(a => a.AddOnId == addOnId);
                    if (addOn is null)
                        return (false, $"AddOnId {addOnId} not found for ItemId {ItemId}");
                    unitPrice += addOn.ExtraPrice;
                }

                // Add Order Item
                orderItems.Add(new OrderItem
                {
                    OrderItemId = itemSeq++,
                    ItemId = ItemId,
                    Quantity = Qty,
                    UnitPrice = unitPrice,
                    SelectedAddOnIds = AddOnIds,
                    SpecialNotes = notes
                });
            }
            // make new order
            var order = new Order
            {
                OrderId = DataSeeder.NextOrderId++,
                OrderType = orderType,
                BranchId = BranchId,
                CustomerId = CustomerId,
                DateTime = DateTime.UtcNow,
                DeliveryAddress = deliveryAddress,
                HandledByEmployeeId = employeeId,
                OrderStatus = OrderStatus.Pending,
                OrderItems = orderItems,
                TotalAmount = orderItems.Sum(x => x.UnitPrice * x.Quantity),
            };
            DataSeeder.NextOrderItemId = itemSeq;
            DataSeeder.Orders.Add(order);

            if (orderType == OrderType.Delivery)
            {
                DataSeeder.Deliveries.Add(new Delivery
                {
                    DeliveryId = DataSeeder.NextDeliveryId++,
                    OrderId = order.OrderId,
                    DeliveryAddress = deliveryAddress!,
                    Status = DeliveryStatus.AwaitingAssignment
                });
            }
            return (true, $"Order placed successfully with OrderId {order.OrderId}");

        }

        public static (bool Success,string Message) StartPreparing(int OrderId,int ChefId,int? ManagerOverrideId = null)
        {
            var Order = DataSeeder.Orders.FirstOrDefault(i => i.OrderId == OrderId);
            if (Order is null)
                return (false, "Order not Found");

            if (Order.OrderStatus != OrderStatus.Pending)
                return (false, "Order must be in Pending Status");

            var chef = DataSeeder.Employees.FirstOrDefault(i => i.EmployeeId == ChefId);
            if (chef is null || chef is not Chef)
                return (false, "Only Chef can prepare Orders");

            if (!chef.AssignedBranchIds.Contains(Order.BranchId))
                return (false, "Chef is not Assigned to this Branch");

            bool isSufficient = InventoryServices.IsSufficient(Order.BranchId, Order.OrderItems);
            if (!isSufficient)
            {
                if (ManagerOverrideId is null)
                    return (false, "Insufficient Stock");

                var Manager = DataSeeder.Employees.FirstOrDefault(i =>i.EmployeeId == ManagerOverrideId);
                if (Manager is null || Manager is not BranchManager)
                    return (false, "Override Denied : Not a branch Manager");

                if (!Manager.AssignedBranchIds.Contains(Order.BranchId))
                    return (false, "Override Denied : Manager is From Different Branch");
            }
            InventoryServices.DeductInventory(Order.BranchId, Order.OrderItems);
            Order.OrderStatus = OrderStatus.Preparing;
            return (true, "Order is being Prepared");
        }
    }
}
