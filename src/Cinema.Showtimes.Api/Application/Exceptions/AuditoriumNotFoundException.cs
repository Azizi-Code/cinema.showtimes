using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class AuditoriumNotFoundException(int id) : NotFoundException($"Auditorium with id '{id}' does not exist.");