using API.Entities;

namespace API.Interfaces
{
    public interface IStockRepository
    {
        void Update(Stock stock);
        Task<Stock> AddStockAsync(Stock stock);
        Task<bool> SaveAllAsync();
    }
}