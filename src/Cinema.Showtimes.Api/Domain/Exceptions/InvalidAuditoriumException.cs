using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class InvalidAuditoriumException : UnprocessableEntityException
{
    public InvalidAuditoriumException() : base("The selected auditorium does not match the show's auditorium.")
    {
    }
}