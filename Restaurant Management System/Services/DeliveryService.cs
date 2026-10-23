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
    public static class DeliveryService
    {
        public static (bool success, string message) AssignStaff(int orderId,int deliveryStaffId,int assignedByEmp)
        {
            var order = DataSeeder.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return (false, "Order is Not Found");
            //Delivery staff assigned to served delivery orders only
            if (order.OrderType != OrderType.Delivery)
                return (false, "Order is Not a Delivery Order");
            if (order.OrderStatus != OrderStatus.Served)
                return (false, "Delivery Staff can only assigned to Served Orders");

            var employee = DataSeeder.Employees.FirstOrDefault(e => e.EmployeeId == assignedByEmp);
            if (employee == null)
                return (false, "Employee is Not Found");
            // Only Waiters, Cashiers, or Branch Managers can assign
            if (employee is not Waiter && employee is not Cashier && employee is not BranchManager)
                return (false, "Only Waiters, Cashiers, or Branch Managers can assign delivery Staff");
            if (!employee.AssignedBranchIds.Contains(order.BranchId))
                return (false, "Employee is not Assigned to this Branch");

            var staff = DataSeeder.DeliveryStaffList.FirstOrDefault(d => d.StaffId == deliveryStaffId);
            if (staff == null)
                return (false, "Delivery Staff is Not Found");
            //Delivery staff must be available
            if (!staff.IsAvailable)
                return (false, "Delivery Staff is currently unavailable");
            //Delivery staff must be from same branch
            if (staff.BranchId != order.BranchId)
                return (false, "Delivery staff must be from same branch as the Order");

            //Only assigned delivery staff can update delivery status
            var delivery = DataSeeder.Deliveries.FirstOrDefault(d => d.OrderId == orderId);
            if (delivery == null)
                return (false, "Delivery Record is Not Found");

            delivery.DeliveryStaffId = deliveryStaffId;
            delivery.Status = DeliveryStatus.OnTheWay;
            staff.IsAvailable = false;
            return (true, $"Delivery assigned to {staff.FullName}, Status: On The Way");
        }

        public static (bool success, string message) MarkDelivered(int deliverId, int deliveryStaffId)
        {
            var delivery = DataSeeder.Deliveries.FirstOrDefault(d => d.DeliveryId == deliverId);
            if (delivery == null)
                return (false, "Delivery Record is Not Found");
            if (delivery.Status != DeliveryStatus.OnTheWay)
                return (false, "Delivery Can Not be Marked on Delivered");
            if (delivery.DeliveryStaffId != deliveryStaffId)
                return (false,"Only Assigned Delivery Staff Member can update this Delivery");

            delivery.Status = DeliveryStatus.Delivered;
            delivery.DeliveryTime = DateTime.Now;

            var order = DataSeeder.Orders.FirstOrDefault(r=>r.OrderId == delivery.OrderId);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Completed;
                var customer = DataSeeder.Customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
                if (customer != null)
                    customer.LoyaltyPoints += (int)Math.Floor(order.TotalAmount);
            }
            var staff = DataSeeder.DeliveryStaffList.FirstOrDefault(d => d.StaffId == deliveryStaffId);
            if (staff != null)
                staff.IsAvailable = true;
            return (true, $"Delivery {delivery.OrderId} is Delivered, Order {order!.OrderId} is Completed");
        }

        public static (bool success, string message) MarkFailed(int deliverId, int deliveryStaffId, string FailureReason)
        {
            var delivery = DataSeeder.Deliveries.FirstOrDefault(d => d.DeliveryId == deliverId);
            if (delivery == null)
                return (false, "Delivery Record is Not Found");
            if (delivery.Status != DeliveryStatus.OnTheWay)
                return (false, "Delivery Can Not be Marked on Delivered");
            if (delivery.DeliveryStaffId != deliveryStaffId)
                return (false, "Only Assigned Delivery Staff Member can update this Delivery");

            if (string.IsNullOrEmpty(FailureReason))
                return (false,"Failure Reason is Required");

            delivery.Status = DeliveryStatus.Failed;
            delivery.FailureReason = FailureReason;
            delivery.DeliveryTime = DateTime.Now;

            var order = DataSeeder.Orders.FirstOrDefault(r => r.OrderId == deliverId);
            if (order != null)
                order.OrderStatus = OrderStatus.Cancelled;

            var staff = DataSeeder.DeliveryStaffList.FirstOrDefault(d => d.StaffId == deliveryStaffId);
            if (staff != null)
                staff.IsAvailable = true;
            return (true, $"Delivery {delivery.OrderId} Failed! Order {order!.OrderId} is Canceled");
        }
    }
}
