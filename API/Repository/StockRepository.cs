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
        public async Task<Stock> AddProductAsync(Stock stock)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
        }
    }
}