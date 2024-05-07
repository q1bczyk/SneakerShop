namespace API.Errors
{
    public class OtherException : Exception
    {
        public OtherException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}