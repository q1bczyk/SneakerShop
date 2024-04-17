namespace API.Helpers
{
    public class FileUploadResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? FileUrl { get; set; }

        public FileUploadResult(bool success, string error, string fileUrl)
        {
            Success = success;
            Error = error;
            FileUrl = fileUrl;
        }

    }
}