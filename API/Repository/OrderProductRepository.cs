using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly DataContext _context;

        public OrderProductRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<OrderProduct> AddOrderAsync(OrderProduct orderProduct)
        {
            await _context.OrderProducts.AddAsync(orderProduct);
            await SaveAllAsync();
            return orderProduct;
        }

        public async Task<bool> DeleteOrderAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Remove(orderProduct);
            await SaveAllAsync();
            return true;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(OrderProduct orderProduct)
        {
            _context.Entry(orderProduct).State = EntityState.Modified;
        }
    }
}