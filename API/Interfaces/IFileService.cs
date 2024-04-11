namespace API.Interfaces
{
    public interface IFileService
    {
        public Task<bool> UploadFileAsync(IFormFile []files, string productName, string producer);
        public Task DeleteFileAsync(string url);
        public Task GeneratePublicLink(string url);
    }
}