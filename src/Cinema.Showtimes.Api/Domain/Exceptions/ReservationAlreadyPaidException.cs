using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class ReservationAlreadyPaidException : UnprocessableEntityException
{
    public ReservationAlreadyPaidException() : base("This reservation has already been paid.")
    {
    }
}