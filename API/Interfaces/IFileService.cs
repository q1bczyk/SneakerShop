namespace API.Interfaces
{
    public interface IFileService
    {
        public Task UploadFileAsync(IFormFile []files, string productName);
        public Task DeleteFileAsync(string url);
        public Task GeneratePublicLink(string url);
    }
}