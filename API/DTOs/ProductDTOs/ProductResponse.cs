using API.DTOs.PhotoDTOs;
using API.DTOs.StockDTOs;

namespace API.DTOs.ProductDTOs
{
    public class ProductResponse : ProductBase
    {
        public string Id { get; set; }
        public List<StockResponse> Stocks { get; set; }
        public List<PhotoResponse> Photos { get; set; }
    }
}