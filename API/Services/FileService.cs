using API.Helpers;
using API.Interfaces;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly BlobContainerClient FilesContainer;
        private readonly string AccountName;
        private readonly string Key;
        private readonly string ContainerName;
        public FileService(IOptions<BlobStorageConfig> config)
        {
            string AccountName = config.Value.AccountName;
            string Key = config.Value.Key;
            string ContainerName = config.Value.ContainerName;

            string blobConnection = $"DefaultEndpointsProtocol=https;AccountName={AccountName};AccountKey={Key};EndpointSuffix=core.windows.net";

            FilesContainer = new BlobContainerClient(blobConnection, ContainerName);
        }
        public async Task<bool> DeleteFileAsync(string imgPath)
        {
            var uri = new Uri(imgPath);
            var blobPath = uri.LocalPath.TrimStart('/').Replace($"{ContainerName}/", "");
            var fileToDelete = FilesContainer.GetBlobClient(blobPath);

            return await fileToDelete.DeleteIfExistsAsync();
        }

        public async Task<string> GeneratePublicLink(string imgUrl)
        {
            var blobClient = new BlobClient(new Uri(imgUrl), new StorageSharedKeyCredential(AccountName, Key));

            var sasBuilder = new BlobSasBuilder
            {
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
                Resource = "b"
            };

            sasBuilder.SetPermissions("rw");

            var sasToken = blobClient.GenerateSasUri(sasBuilder);
            var publicUrl = sasToken.ToString();

            return publicUrl;
        }

        public async Task<FileUploadResult> UploadFileAsync(IFormFile file, string productName, string producer, int index)
        {
            if(!IsFileExtensionAllowed(file))
                return new FileUploadResult(false, "Wrong file extension", null);

            using(Stream stream = file.OpenReadStream())
            {
                string fileName = GenerateFileName(file, productName, producer, index);
            
                await FilesContainer.UploadBlobAsync(fileName, stream);
                string fileUrl = FilesContainer.GetBlobClient(fileName).Uri.ToString();
                
                return new FileUploadResult(true, null, fileUrl);
            }
        }

        private bool IsFileExtensionAllowed(IFormFile file)
        {
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
            string extension = Path.GetExtension(file.FileName)?.ToLower();
            return allowedExtensions.Contains(extension);
        }

        private string GenerateFileName(IFormFile file, string productName, string producer, int index)
        {
            string fileExtension = "." + Path.GetExtension(file.FileName)?.TrimStart('.').ToLower();

            string fileName = $"{producer}/{productName}/{productName + index + fileExtension}";

            return fileName.Replace(" ", "");
        }
    }
}