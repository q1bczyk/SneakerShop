using API.Entities;

namespace API.Interfaces
{
    public interface IStockOrderRepository
    {
        void Update(StockOrder stockOrder);
        Task<StockOrder> AddOrderAsync(StockOrder stockOrder);
        Task<bool> DeleteOrderAsync(StockOrder stockOrder);
        Task<bool> SaveAllAsync();
    }
}