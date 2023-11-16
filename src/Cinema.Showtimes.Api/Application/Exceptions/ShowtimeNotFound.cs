using Cinema.Showtimes.Api.Application.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class ShowtimeNotFound : NotFoundException
{
    public ShowtimeNotFound(int id) : base($"Movie with id '{id}' does not exist.")
    {
    }
}