using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        void Update(Product product);
        Task<Product> AddProductAsync(Product product);
        Task<bool> SaveAllAsync();
    }
}