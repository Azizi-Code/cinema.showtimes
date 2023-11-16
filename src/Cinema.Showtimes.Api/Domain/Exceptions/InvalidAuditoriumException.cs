using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class InvalidAuditoriumException : UnprocessableEntityException
{
    public InvalidAuditoriumException(string message) : base(message)
    {
    }
}