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

        private async Task<bool> ProductInStock(OrderStockRequest orderProduct)
        {
            int stockLeft = await StockExists(orderProduct);

            if(stockLeft - orderProduct.Quantity < 0)
                throw new OtherException(400, "Product is already out of stock!");

            return true;
        }

        private async Task<int> StockExists(OrderStockRequest orderProduct)
        {
            var stock = await _stockRepository.GetStock(orderProduct.StockId);

            if(stock == null)
                throw new OtherException(404, "Product does not exist!"); 

            return stock.Quantity;
        }
    }
}