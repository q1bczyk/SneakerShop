using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await SaveAllAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await SaveAllAsync();
            return true;
        }

        public async Task<List<Order>> GetAllOrdersAsync(int page, int pageSize)
        {
            return await _context.Orders
                            .Include(o => o.StockOrder)
                                .ThenInclude(op => op.Stock)
                                .ThenInclude(s => s.Product)
                            .Include(o => o.Contact)
                            .Skip((page - 1) * pageSize)
                            .ToListAsync();
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            return await _context.Orders
                            .Include(o => o.StockOrder)
                                .ThenInclude(op => op.Stock)
                                    .ThenInclude(p => p.Product)
                            .Include(o => o.Contact)
                            .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}