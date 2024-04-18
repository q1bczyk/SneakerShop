using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await SaveAllAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(string productId)
        {
            var productToDelete = await _context.Products
                                            .FirstOrDefaultAsync(p => p.Id == productId);
            _context.Remove(productToDelete);
            await SaveAllAsync();

            return true;
        }

        public async Task<Product> GetProductsById(string productId)
        {
            return await _context.Products
                .Include(p => p.Stocks)
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await SaveAllAsync();
        }
    }
}