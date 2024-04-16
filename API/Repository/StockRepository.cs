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

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
        }
    }
}