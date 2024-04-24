using API.DTOs.OrderDTOs;
using API.Errors;
using API.Interfaces;

namespace API.Extensions
{
    public class PlaceOrderPossibilityExtension
    {
        private readonly IStockRepository _stockRepository;

        public PlaceOrderPossibilityExtension(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<bool> PlaceOrderPossibility(OrderRequest orderRequest)
        {
            foreach(var product in orderRequest.Products)
                await ProductInStock(product);

            return true;
        }

        private async Task<bool> ProductInStock(OrderProductRequest orderProduct)
        {
            int stockLeft = await StockExists(orderProduct);

            if(stockLeft - orderProduct.Quantity < 0)
                throw new StockException(400, "Product is already out of stock!");

            return true;
        }

        private async Task<int> StockExists(OrderProductRequest orderProduct)
        {
            var stock = await _stockRepository.GetStock(orderProduct.StockId);

            if(stock == null)
                throw new StockException(404, "Product does not exist!"); 

            return stock.Quantity;
        }
    }
}