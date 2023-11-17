namespace Cinema.Showtimes.Api.Infrastructure.Exceptions;

public class RedisServiceException : Exception
{
    public RedisServiceException(string message) : base(message)
    {
    }
}