using API.Entities;

namespace API.Interfaces
{
    public interface IStockRepository
    {
        void Update(Stock stock);
        Task<Stock> AddProductAsync(Stock stock);
        Task<bool> SaveAllAsync();
    }
}