using API._Controllers;
using API.DTOs.ProductDTOs;
using API.DTOs.ProductDTOs.StockDTOs;
using API.DTOs.StockDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "RequireModeratorRole")]
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

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpPut("{stockId}")]
        public async Task<ActionResult<ProductResponse>> EditStock(string stockId, StockPUTRequest stockPUTRequest)
        {
            var stockToEdit = await _stockRepository.GetStock(stockId);

            if(stockToEdit == null)
                return NotFound("Size does not exist!");

            stockToEdit.Discount = stockPUTRequest.Discount;
            stockToEdit.Quantity = stockPUTRequest.Quantity;
            _stockRepository.Update(stockToEdit);
            await _stockRepository.SaveAllAsync();

            return Ok(_mapper.Map<StockResponse>(stockToEdit));
        }

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpDelete("{stockId}")]
        public async Task<ActionResult<string>> DeleteStock(string stockId)
        {
            var stockToDelete = await _stockRepository.GetStock(stockId);
            
            if(stockToDelete == null)
                return NotFound("Stock does not exist!");
            
            await _stockRepository.DeleteStockAsync(stockToDelete);

            return Ok("Stock has been deleted successful!");
        } 
    }
}