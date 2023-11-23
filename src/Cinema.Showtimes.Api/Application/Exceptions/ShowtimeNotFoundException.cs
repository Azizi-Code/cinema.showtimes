using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class ShowtimeNotFoundException : NotFoundException
{
    public ShowtimeNotFoundException(int id) : base($"ShowTime with id '{id}' does not exist.")
    {
    }
}