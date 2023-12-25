using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class ShowtimeNotFoundException(int id) : NotFoundException($"ShowTime with id '{id}' does not exist.");