using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class SeatsAlreadyReservedException : UnprocessableEntityException
{
    public SeatsAlreadyReservedException() : base("One or more selected seats are already reserved.")
    {
    }
}