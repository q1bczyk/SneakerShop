using API.Helpers;

namespace API.Interfaces
{
    public interface IFileService
    {
        public Task<FileUploadResult> UploadFileAsync(IFormFile file, string productName, string producer, int index);
        public Task<bool> DeleteFileAsync(string imgPath);
        public Task<string> GeneratePublicLink(string imgUrl);
    }
}