namespace API.Errors
{
    public class StockException : Exception
    {
        public StockException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}