using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task Update(Product product);
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductsById(string productId);
        Task<List<Product>> GetProducts(int page, int amount);
        Task<bool> DeleteProductAsync(Product product);
        Task<bool> SaveAllAsync();
    }
}