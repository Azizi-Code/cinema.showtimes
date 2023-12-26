using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class AuditoriumAndShowTimeAuditoriumNotMatchedException(int id)
    : UnprocessableEntityException($"Auditorium with id '{id}' does not matched with showtime auditorium.");