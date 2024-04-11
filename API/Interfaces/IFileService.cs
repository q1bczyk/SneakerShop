namespace API.Interfaces
{
    public interface IFileService
    {
        public Task<bool> UploadFileAsync(IFormFile []files, string productName, string producer);
        public Task<bool> DeleteFileAsync(string imgPath);
        public Task<string> GeneratePublicLink(string imgUrl);
    }
}