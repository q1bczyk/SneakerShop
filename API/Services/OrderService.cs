using API.DTOs.OrderDTOs;
using API.Entities;
using API.Extensions;
using API.Extensions.OrderExtensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly PlaceOrderPossibilityExtension _placOrderPossibilityExtension;
        private readonly PlaceOrderExtension _placeOrderExtension;
        
        public OrderService(PlaceOrderPossibilityExtension placOrderPossibilityExtension, PlaceOrderExtension placeOrderExtension) 
        {
            _placOrderPossibilityExtension = placOrderPossibilityExtension;
            _placeOrderExtension = placeOrderExtension;
        }

        public async Task<Order> PlaceOrderAsync(OrderRequest orderRequest)
        {
            await _placOrderPossibilityExtension.PlaceOrderPossibility(orderRequest);
            return await _placeOrderExtension.PlaceOrder(orderRequest);
        }

    }
}