using API.Entities;

namespace API.Interfaces
{
    public interface IOrderProductRepository
    {
        void Update(OrderProduct orderProduct);
        Task<OrderProduct> AddOrderAsync(OrderProduct orderProduct);
        Task<bool> DeleteOrderAsync(OrderProduct orderProduct);
        Task<bool> SaveAllAsync();
    }
}