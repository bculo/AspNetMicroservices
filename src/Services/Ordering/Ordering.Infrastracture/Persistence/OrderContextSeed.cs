using Microsoft.Extensions.Logging;
using Ordering.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "Culix", FirstName = "Bozo", LastName = "Culo", EmailAddress = "Culix123123123@gmail.com", AddressLine = "FakeAdress", Country = "Croatia", TotalPrice = 350 }
            };
        }
    }
}
