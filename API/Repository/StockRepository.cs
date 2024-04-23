using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly DataContext _context;

        public StockRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Stock> AddStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await SaveAllAsync();
            return stock;
        }

        public async Task<Stock> GetStock(string stockId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(
                x => x.Id == stockId
            );
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> StockExists(string productId, float size)
        {
            return await _context.Stocks.AnyAsync(
                    x => x.ProductId == productId && 
                    x.Size == size);
        }

        public void Update(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
        }
    }
}