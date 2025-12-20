namespace Tracker.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public int StatusCode { get; }
        public DomainException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}