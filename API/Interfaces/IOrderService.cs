using API.DTOs.OrderDTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(OrderRequest orderRequest);
    }
}