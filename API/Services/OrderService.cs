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
        private readonly IEmailService _emailService;

        public OrderService(PlaceOrderPossibilityExtension placOrderPossibilityExtension, PlaceOrderExtension placeOrderExtension, CheckContactExtension checkContactExtension, IEmailService emailService)
        {
            _placOrderPossibilityExtension = placOrderPossibilityExtension;
            _placeOrderExtension = placeOrderExtension;
            _checkContactExtension = checkContactExtension;
            _emailService = emailService;
        }

        public async Task<Order> PlaceOrderAsync(OrderRequest orderRequest, string userId, string email)
        {
            if(await _checkContactExtension.CheckContact(userId, orderRequest.ContactId) == false)
                throw new ControlledException(404, "Contact does not exists!");

            await _placOrderPossibilityExtension.PlaceOrderPossibility(orderRequest);
            var newOrder =  await _placeOrderExtension.PlaceOrder(orderRequest);
            await _emailService.SendOrderConfirmationAsync(email, newOrder.Id);

            return newOrder;
        }

    }
}