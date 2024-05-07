using API.DTOs.ProductDTOs;

namespace API.DTOs.StockDTOs
{
    public class StockResponse : StockBase
    {
        public string Id { get; set; }
        public float Size { get; set; }
    }
}