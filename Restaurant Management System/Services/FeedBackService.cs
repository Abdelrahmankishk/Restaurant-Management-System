using Restaurant_Management_System.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Management_System.Services
{
    public static class FeedBackService
    {
        public static (bool success, string Message) Submit(int customerId, int orderId, string comment, int rating)
        {
            var order = DataSeeder.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                return (false, "Order is Not Found");
            if (order.CustomerId != customerId)
                return (false, $"Order Does not belong to Customer with id {customerId}");
            if (order.OrderStatus != Models.Enums.OrderStatus.Completed)
                return (false, "Feedback can only be submitted for Completed orders");

            bool isSubmitted = DataSeeder.Feedbacks.Any(f => f.CustomerId == customerId && f.OrderId == orderId);
            if (isSubmitted)
                return (false, "Feedback is already Submitted for this Order");
            if (rating < 1 || rating > 5)
                return (false, "Rating Must be between 1 and 5");

            DataSeeder.Feedbacks.Add(new Models.Feedback
            {
                FeedBackId = DataSeeder.NextFeedbackId++,
                Comments = comment,
                OrderId = orderId,
                CustomerId = customerId,
                Rating = rating,
                Date = DateTime.Now
            });

            return (true, "Thank You! Your Feedback has been Submitted Successfully");
        }
    }
}
