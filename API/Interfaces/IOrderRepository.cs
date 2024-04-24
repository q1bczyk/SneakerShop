using API.Entities;

namespace API.Interfaces
{
    public interface IOrderRepository
    {
        void Update(Order order);
        Task<Order> AddOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(Order order);
        Task<Order> GetOrderAsync(string id);
        Task<List<Order>> GetAllOrdersAsync(int page, int pageSize);
        Task<bool> SaveAllAsync();
    }
}