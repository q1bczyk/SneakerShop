using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StockOrderRepository : IStockOrderRepository
    {
        private readonly DataContext _context;

        public StockOrderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<StockOrder> AddOrderAsync(StockOrder stockOrder)
        {
            await _context.StockOrders.AddAsync(stockOrder);
            await SaveAllAsync();
            return stockOrder;
        }

        public async Task<bool> DeleteOrderAsync(StockOrder stockOrder)
        {
            _context.StockOrders.Remove(stockOrder);
            await SaveAllAsync();
            return true;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(StockOrder stockOrder)
        {
            _context.Entry(stockOrder).State = EntityState.Modified;
        }
    }
}