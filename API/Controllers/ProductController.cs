using API._Controllers;
using API.DTOs.ProductDTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IFileService _fileService;

        public ProductController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductRequest productRequest)
        {
            await _fileService.UploadFileAsync(productRequest.files, "Nike");
            return Ok("Succes");
        }

    }
}