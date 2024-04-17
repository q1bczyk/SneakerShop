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
                var fileUploadResult = await _fileService.UploadFileAsync(productRequest.Files[i], addedProduct.Model, addedProduct.Producer, i);

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

    }
}