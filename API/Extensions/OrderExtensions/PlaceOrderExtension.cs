using API.DTOs.OrderDTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions.OrderExtensions
{
    public class PlaceOrderExtension
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IOrderProductRepository _orderProductRepository;

        public PlaceOrderExtension(IOrderRepository orderRepository, IStockRepository stockRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository;
            _stockRepository = stockRepository;
            _orderProductRepository = orderProductRepository;
        }

        public async Task<Order> PlaceOrder(OrderRequest orderRequest)
        {
            return await AddOrderProduct(orderRequest);
        }

        private async Task<Order> AddOrderProduct(OrderRequest orderRequest)
        {
            Order order = await AddOrder(orderRequest);

            foreach(var product in orderRequest.Products)
            {
                OrderProduct orderProduct = new OrderProduct
                {
                    ProductId = product.StockId,
                    OrderId = order.Id,
                    Quantity = product.Quantity,
                };

                await _orderProductRepository.AddOrderAsync(orderProduct);
            }

            return order;
        }

        private async Task<Order> AddOrder(OrderRequest orderRequest)
        {
            var ordeerToAdd = new Order
            {
                Date = DateTime.UtcNow,
                Status = "nowe",
                ContactId = orderRequest.ContactId,
            };
            var addedOrder = await _orderRepository.AddOrderAsync(ordeerToAdd);
            return addedOrder;
        }
    }
}