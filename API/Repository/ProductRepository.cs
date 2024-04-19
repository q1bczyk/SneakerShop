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

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Remove(product);
            await SaveAllAsync();

            return true;
        }

        public async Task<List<Product>> GetProducts(int page, int amount)
        {
            return await _context.Products
                            .Skip((page - 1) * amount)
                            .Select(product => new Product
                            {
                                Id = product.Id,
                                Producer = product.Producer,
                                Model = product.Model,
                                Price = product.Price,
                                Stocks = product.Stocks,
                                Photos = product.Photos.Where(p => p.ProfilePhoto == true).ToList(),
                            })
                            .ToListAsync();
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