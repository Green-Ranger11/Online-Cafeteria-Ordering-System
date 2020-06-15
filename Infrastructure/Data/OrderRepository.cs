using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderAsync(int orderId, string buyerEmail)
        {
            return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(c => c.Ingrediants)
            .Include(o => o.DeliveryMethod)
            .SingleOrDefaultAsync(o => o.Id == orderId && o.BuyerEmail == buyerEmail);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(string buyerEmail)
        {
            return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(c => c.Ingrediants)
            .Include(o => o.DeliveryMethod)
            .Where(o => o.BuyerEmail == buyerEmail)
            .ToListAsync();
        }
    }
}