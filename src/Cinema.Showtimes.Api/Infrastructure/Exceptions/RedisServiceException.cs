namespace Cinema.Showtimes.Api.Infrastructure.Exceptions;

public class RedisServiceException(string message) : Exception(message);