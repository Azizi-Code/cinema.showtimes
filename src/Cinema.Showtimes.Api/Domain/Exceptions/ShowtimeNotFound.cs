using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class ShowtimeNotFound : NotFoundException
{
    public ShowtimeNotFound(int id) : base($"Movie with id '{id}' does not exist.")
    {
    }
}