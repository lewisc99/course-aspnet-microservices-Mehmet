using Microsoft.EntityFrameworkCore;
using ordering.domain.Entities;
using ordering.infrastructure.Persistence;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordering.infrastructure.Repositories
{
   public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        //because the OrderRepository inherent from repository base and 
        //repository base has a dependency injection of OrderContext
        //wee need to add using this form the dbContext to access the methods in it.
        //is like adding a property - private readonly DbContext _dbContext;
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                                .Where(o => o.UserName == userName)
                                .ToListAsync();
            return orderList;

            
        }

        
    }
}
