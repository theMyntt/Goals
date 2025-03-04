namespace Goals.API.Exceptions
{
    public class HttpException : Exception
    {
        public int StatusCode { get; private set; }

        public HttpException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
