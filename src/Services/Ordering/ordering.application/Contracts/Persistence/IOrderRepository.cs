using System.Collections.Generic;

using System.Threading.Tasks;
using ordering.domain.Entities;

using ordering.application.Contracts.Persistence;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}