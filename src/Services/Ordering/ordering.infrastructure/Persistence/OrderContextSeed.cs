using Microsoft.Extensions.Logging;
using ordering.domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordering.infrastructure.Persistence
{
    public class OrderContextSeed
    {

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler",Country = "Turkey", TotalPrice = 350}
            };
        }



        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {

            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }

           
        }

       
    }
}
