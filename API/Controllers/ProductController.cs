using API._Controllers;
using API.DTOs.ProductDTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public ProductController(IFileService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ProductRequest>> AddProduct(ProductRequest productRequest)
        {
            return Ok(productRequest);
        }

    }
}