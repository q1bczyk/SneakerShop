using API._Controllers;
using API.DTOs.ProductDTOs;
using API.DTOs.StockDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StockController : BaseApiController
    {
        
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public StockController(IMapper mapper, IProductRepository productRepository, IStockRepository stockRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        [HttpPost("{ProductId}")] 
        public async Task<ActionResult<ProductResponse>> AddStock(StockRequest stockRequest, string productId)
        {
            var product = await _productRepository.GetProductById(productId);
            
            if(product == null)
                return NotFound("Product does not exist!");

            if(await _stockRepository.StockExists(productId, stockRequest.Size) == true)
                return BadRequest("Size already exists!");

            var stockToAdd = _mapper.Map<Stock>(stockRequest);
            stockToAdd.ProductId = productId;

            await _stockRepository.AddStockAsync(stockToAdd);

            return Ok(_mapper.Map<ProductResponse>(product));        
        }
    }
}