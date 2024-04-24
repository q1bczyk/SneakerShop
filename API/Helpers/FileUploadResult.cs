namespace API.Helpers
{
    public class FileUploadResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? FileUrl { get; set; }

        public FileUploadResult(bool success, string error)
        {
            Success = false;
            Error = error;
        }

        public FileUploadResult(bool success, string error, string fileUrl)
        {
            Success = true;
            Error = error;
            FileUrl = fileUrl;
        }

    }
}