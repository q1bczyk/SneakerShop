using API.Helpers;
using API.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly BlobContainerClient FilesContainer;
        public FileService(IOptions<BlobStorageConfig> config)
        {
            string AccountName = config.Value.AccountName;
            string Key = config.Value.Key;
            string ContainerName = config.Value.ContainerName;

            string blobConnection = $"DefaultEndpointsProtocol=https;AccountName={AccountName};AccountKey={Key};EndpointSuffix=core.windows.net";

            FilesContainer = new BlobContainerClient(blobConnection, ContainerName);
        }
        public Task DeleteFileAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task GeneratePublicLink(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UploadFileAsync(IFormFile[] files, string productName, string producer)
        {
             for(int i = 0; i < files.Length; i++)
             {
                if(IsFileExtensionAllowed(files[i]))
                    return false;

                using(Stream stream = files[i].OpenReadStream())
                {
                    await FilesContainer.UploadBlobAsync(GenerateFileName(files[i], productName, producer, i), stream);
                }
             }

             return true;
        }

        private bool IsFileExtensionAllowed(IFormFile file)
        {
            string[] allowedExtensions = {".jpg", ".jpeg", ".png"};
            string extension = Path.GetExtension(file.FileName)?.ToLower();
            return allowedExtensions.Contains(extension);
        }

        private string GenerateFileName(IFormFile file, string productName, string producer, int index)
        {
            string fileExtension = "." + Path.GetExtension(file.FileName)?.TrimStart('.').ToLower();

            string fileName = $"{producer}/{productName}/{productName + index + fileExtension}";

            return fileName;
        }
    }
}