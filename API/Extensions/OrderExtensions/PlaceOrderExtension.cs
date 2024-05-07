using API.DTOs.OrderDTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions.OrderExtensions
{
    public class PlaceOrderExtension
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IStockOrderRepository _stockOrderRepository;

        public PlaceOrderExtension(IOrderRepository orderRepository, IStockRepository stockRepository, IStockOrderRepository stockOrderRepository)
        {
            _orderRepository = orderRepository;
            _stockRepository = stockRepository;
            _stockOrderRepository = stockOrderRepository;
        }

        public async Task<Order> PlaceOrder(OrderRequest orderRequest)
        {
            return await AddStockOrder(orderRequest);
        }

        private async Task<Order> AddStockOrder(OrderRequest orderRequest)
        {
            Order order = await AddOrder(orderRequest);

            foreach(var product in orderRequest.Products)
            {
                StockOrder StockOrder = new StockOrder
                {
                    StockId = product.StockId,
                    OrderId = order.Id,
                    Quantity = product.Quantity,
                };

                await _stockOrderRepository.AddOrderAsync(StockOrder);
                await UpdateStock(product.StockId, product.Quantity);
            }

            return order;
        }

        private async Task UpdateStock(string stockId, int quantity)
        {
            var stockToUpdate = await _stockRepository.GetStock(stockId);
            stockToUpdate.Quantity -= quantity;

            _stockRepository.Update(stockToUpdate);
            await _stockRepository.SaveAllAsync();
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