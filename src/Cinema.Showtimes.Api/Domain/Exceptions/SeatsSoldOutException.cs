using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class SeatsSoldOutException : UnprocessableEntityException
{
    public SeatsSoldOutException() : base("One ore more selected seats are soldOut.")
    {
    }
}