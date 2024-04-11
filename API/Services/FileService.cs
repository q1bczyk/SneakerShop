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

        public async Task UploadFileAsync(IFormFile[] files, string productName)
        {
             int index = 1;

            foreach(IFormFile file in files)
            {
                string fileExtension = "." + Path.GetExtension(file.FileName)?.TrimStart('.').ToLower();

                using(Stream stream = file.OpenReadStream())
                {
                    await FilesContainer.UploadBlobAsync($"{productName}/{productName + index + fileExtension}", stream);
                    index++;
                }
            }
        }

        // private bool IsFileExtensionAllowed(IFormFile file)
        // {
            
        // }
    }
}