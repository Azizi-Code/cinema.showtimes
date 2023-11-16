using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class InvalidAuditoriumException : UnprocessableEntityException
{
    public InvalidAuditoriumException() : base("The selected auditorium does not match the show's auditorium.")
    {
    }
}