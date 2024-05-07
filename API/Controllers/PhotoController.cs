using API._Controllers;
using API.DTOs.ProductDTOs.PhotoDTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.Helpers;
using API.Entities;
using API.DTOs.PhotoDTOs;

namespace API.Controllers
{
    public class PhotoController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IFileService _fileService;

        public PhotoController(IMapper mapper, IProductRepository productRepository, IPhotoRepository photoRepository, IFileService fileService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _photoRepository = photoRepository;
            _fileService = fileService;
        }

        [HttpPost("{productId}")]
        public async Task<ActionResult<string>> AddPhoto(PhotoRequest photoRequest, string productId)
        {
            var product = await _productRepository.GetProductById(productId);
            
            if(product == null)
                return NotFound("Product does not exist!");

            foreach(var file in photoRequest.Files)
            {
                FileUploadResult fileUploadResult = await _fileService.UploadFileAsync(file, product.Model, product.Producer, product.Color);
                
                if(!fileUploadResult.Success)
                    return BadRequest(fileUploadResult.Error);

                var photoToAdd = new Photo
                {
                    ImgUrl = fileUploadResult.FileUrl,
                    ProfilePhoto = false,
                    ProductId = product.Id
                };

                await _photoRepository.AddPhotoAsync(photoToAdd);
            }

            return Ok("Photo has been added successful!");
        }

        [HttpDelete("{photoId}")]
        public async Task<ActionResult<string>> DeletePhoto(string photoId)
        {            
            var photoToDelete = await _photoRepository.GetPhotoByIdAsync(photoId);
            
            if(photoToDelete == null)
                return NotFound("Photo does not exist!");

            await _fileService.DeleteFileAsync(photoToDelete.ImgUrl);

            await _photoRepository.DeletePhotoAsync(photoToDelete);

            return Ok("Photo has been deleted successful!");
        }

        [HttpPut("{photoId}")]
        public async Task<ActionResult<PhotoResponse>> ChangeProfilePhoto(string photoId, string productId)
        {
            var currentProfilePhoto = await _photoRepository.GetProfilePhotoAsync(productId);
            var newProfilePhoto = await _photoRepository.GetPhotoByIdAsync(photoId);

            if(currentProfilePhoto == null)
                return NotFound("Product does not exist!");
            if(newProfilePhoto == null)
                return NotFound("Photo does not exist!");

            currentProfilePhoto.ProfilePhoto = false;
            _photoRepository.Update(currentProfilePhoto);
            newProfilePhoto.ProfilePhoto = true; 
            _photoRepository.Update(newProfilePhoto);

            await _photoRepository.SaveAllAsync();

            return Ok(_mapper.Map<PhotoResponse>(newProfilePhoto));
        }
    }
}