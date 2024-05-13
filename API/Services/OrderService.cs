using API.DTOs.OrderDTOs;
using API.Entities;
using API.Errors;
using API.Extensions;
using API.Extensions.OrderExtensions;
using API.Interfaces;


namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly PlaceOrderPossibilityExtension _placOrderPossibilityExtension;
        private readonly PlaceOrderExtension _placeOrderExtension;
        private readonly CheckContactExtension _checkContactExtension;

        public OrderService(PlaceOrderPossibilityExtension placOrderPossibilityExtension, PlaceOrderExtension placeOrderExtension, CheckContactExtension checkContactExtension)
        {
            _placOrderPossibilityExtension = placOrderPossibilityExtension;
            _placeOrderExtension = placeOrderExtension;
            _checkContactExtension = checkContactExtension;
        }

        public async Task<Order> PlaceOrderAsync(OrderRequest orderRequest, string userId)
        {
            if(await _checkContactExtension.CheckContact(userId, orderRequest.ContactId) == false)
                throw new ControlledException(404, "Contact does not exists!");

            await _placOrderPossibilityExtension.PlaceOrderPossibility(orderRequest);
            return await _placeOrderExtension.PlaceOrder(orderRequest);
        }

    }
}