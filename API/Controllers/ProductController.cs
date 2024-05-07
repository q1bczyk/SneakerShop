using API._Controllers;
using API.DTOs.Product.PriceDTO;
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
        private readonly IPhotoRepository _photoRepository;

        public ProductController(IFileService fileService, IMapper mapper, IProductRepository productRepository, IStockRepository stockRepository, IPhotoRepository photoRepository)
        {
            _fileService = fileService;
            _mapper = mapper;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _photoRepository = photoRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> AddProduct(ProductRequest productRequest)
        {
            var productToAdd = _mapper.Map<Product>(productRequest);
            var addedProduct = await _productRepository.AddProductAsync(productToAdd);

            for(int i = 0; i < productRequest.Files.Length; i++)
            {
                bool isProfilePhoto = false;
                var fileUploadResult = await _fileService.UploadFileAsync(productRequest.Files[i], addedProduct.Model, addedProduct.Producer, addedProduct.Color);

                if(!fileUploadResult.Success)
                    return BadRequest(fileUploadResult.Error);

                if(i == productRequest.ProfilePhotoIndex)
                    isProfilePhoto = true;

                var photo = new Photo
                {
                    ImgUrl = fileUploadResult.FileUrl,
                    ProfilePhoto = isProfilePhoto,
                    ProductId = addedProduct.Id,
                };

                await _photoRepository.AddPhotoAsync(photo);
            }

            foreach(var stock in productRequest.Stock)
            {
                var stockToAdd = _mapper.Map<Stock>(stock);
                stockToAdd.ProductId = addedProduct.Id;
                await _stockRepository.AddStockAsync(stockToAdd);
            }

            return Ok(_mapper.Map<ProductResponse>(addedProduct));
        }

        [HttpPut("editPrice/{productId}")]
        public async Task<ActionResult<ProductResponse>> EditPrice(string productId, PriceRequest priceRequest)
        {
            var product = await _productRepository.GetProductById(productId);
            if(product == null)
                return NotFound("Product doesn't exists!");

            product.Price = priceRequest.NewPrice;

            await _productRepository.Update(product);

            return Ok(_mapper.Map<ProductResponse>(product));
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<string>> DeleteProduct(string productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if(product == null)
                return NotFound("Product doesn't exist!");

            foreach(var photo in product.Photos)
                await _fileService.DeleteFileAsync(photo.ImgUrl);

            await _productRepository.DeleteProductAsync(product);
            return Ok("Product deleted succesful!");
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(string productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if(product == null)
                return NotFound("Product doesn't exist");

            foreach(var photos in product.Photos)
                photos.ImgUrl = await _fileService.GeneratePublicLink(photos.ImgUrl);

            return Ok(_mapper.Map<ProductResponse>(product));
        }

        [HttpGet("products")]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts(int page, int amount)
        {
            var products = await _productRepository.GetProducts(page, amount);
            
            foreach(var product in products)
                product.Photos[0].ImgUrl = await _fileService.GeneratePublicLink(product.Photos[0].ImgUrl);

            return Ok(_mapper.Map<List<ProductResponse>>(products));
        }

    }
}