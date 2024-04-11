using API.DTOs.StockDTOs;

namespace API.DTOs.ProductDTOs
{
    public class ProductResponse : ProductBase
    {
        public int Id { get; set; }
        public StockResponse StockResponse { get; set; }
    }
}