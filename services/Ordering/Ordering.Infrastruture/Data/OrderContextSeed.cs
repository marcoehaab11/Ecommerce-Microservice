using Microsoft.Extensions.Logging;
using Ordering.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastruture.Data
{
    public class OrderContextSeed

    {
        public static async Task SeedAsync(OrderDbContext orderDbContext,ILogger<OrderContextSeed> logger)
        {
            if (!orderDbContext.Orders.Any())
            {
                orderDbContext.Orders.AddRange(GetOrders());
                await orderDbContext.SaveChangesAsync();
                logger.LogInformation("Seeded Order database");
            }
        }

        public static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
        {
            new ()
            {
                UserName = "Marco",
                FirstName = "Marco",
                LastName = "Polo",
                EmailAddress = "test@gamil.com",
                AddressLine = "Via Roma 1",
                Country = "Italy",
                State = "RM",
                ZipCode = "00100",
                TotalPrice = 350,
                CardHolderName = "Marco Polo",
                CardNumber = "1234567890123456",
                Expiration = "12/25",
                CVV = "123",
                PaymentMethod = "Credit Card"

            }
        };
        }

    }
}