using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class AuditoriumAndShowTimeAuditoriumNotMatchedException : UnprocessableEntityException
{
    public AuditoriumAndShowTimeAuditoriumNotMatchedException(int id) : base(
        $"Auditorium with id '{id}' does not matched with showtime auditorium.")
    {
    }
}