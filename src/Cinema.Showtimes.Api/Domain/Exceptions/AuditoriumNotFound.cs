using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Domain.Exceptions;

public class AuditoriumNotFoundException : NotFoundException
{
    public AuditoriumNotFoundException(int id) : base($"Auditorium with id '{id}' does not exist.")
    {
    }
}