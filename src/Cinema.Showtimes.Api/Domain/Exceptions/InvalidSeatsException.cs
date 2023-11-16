using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class InvalidSeatsException : UnprocessableEntityException
{
    public InvalidSeatsException(string message) : base(message)
    {
    }
}