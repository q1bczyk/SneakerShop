using API.Entities;

namespace API.Interfaces
{
    public interface IStockRepository
    {
        void Update(Stock stock);
        Task<Stock> AddStockAsync(Stock stock);
        Task<bool> StockExists(string productId, float size);
        Task<bool> SaveAllAsync();
    }
}