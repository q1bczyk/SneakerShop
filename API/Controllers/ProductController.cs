using API._Controllers;
using API.DTOs;
using API.DTOs.ProductDTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;

        public ProductController(IFileService fileService, IMapper mapper, IProductRepository productRepository, IStockRepository stockRepository)
        {
            _fileService = fileService;
            _mapper = mapper;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> AddProduct(ProductRequest productRequest)
        {
            Console.WriteLine(productRequest.Stock.Length);
            var productToAdd = _mapper.Map<Product>(productRequest);
            var addedProduct = await _productRepository.AddProductAsync(productToAdd);

            if(!await _fileService.UploadFileAsync(productRequest.Files, productRequest.Model, productRequest.Producer))
                return BadRequest("Wrong file extension!");

            foreach(var stock in productRequest.Stock)
            {
                var stockToAdd = _mapper.Map<Stock>(stock);
                stockToAdd.ProductId = addedProduct.Id;
                await _stockRepository.AddStockAsync(stockToAdd);
            }

            return Ok(_mapper.Map<ProductResponse>(addedProduct));
        }

    }
}