using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
         Task<Order> GetOrderAsync(int orderId, string buyerEmail);
         Task<IReadOnlyList<Order>> GetOrdersAsync(string buyerEmail);
    }
}