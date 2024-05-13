namespace API.Errors
{
    public class ControlledException : Exception
    {
        public ControlledException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}