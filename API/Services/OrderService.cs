using API.DTOs.OrderDTOs;
using API.Entities;
using API.Errors;
using API.Extensions;
using API.Extensions.OrderExtensions;
using API.Interfaces;
using API.Repository;

namespace API.Services
{
    public class OrderService : IOrderService
    {
        private readonly PlaceOrderPossibilityExtension _placOrderPossibilityExtension;
        private readonly PlaceOrderExtension _placeOrderExtension;
        private readonly IContactRepository _contactRepository;
        
        public OrderService(PlaceOrderPossibilityExtension placOrderPossibilityExtension, PlaceOrderExtension placeOrderExtension, IContactRepository contactRepository) 
        {
            _placOrderPossibilityExtension = placOrderPossibilityExtension;
            _placeOrderExtension = placeOrderExtension;
            _contactRepository = contactRepository;
        }

        public async Task<Order> PlaceOrderAsync(OrderRequest orderRequest)
        {
            var contact = await _contactRepository.GetContactByIdAsync(orderRequest.ContactId);

            if(contact == null)
                throw new OtherException(404, "User with Id does not exist!");

            await _placOrderPossibilityExtension.PlaceOrderPossibility(orderRequest);
            return await _placeOrderExtension.PlaceOrder(orderRequest);
        }

    }
}