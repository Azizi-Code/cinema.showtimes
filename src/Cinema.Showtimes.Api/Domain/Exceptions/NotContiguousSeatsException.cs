using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class NotContiguousSeatsException : UnprocessableEntityException
{
    public NotContiguousSeatsException() : base("Selected seats are not contiguous.")
    {
    }
}