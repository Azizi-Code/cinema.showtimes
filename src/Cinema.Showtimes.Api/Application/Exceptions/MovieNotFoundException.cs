using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class MovieNotFoundException(string id) : NotFoundException($"Movie with id '{id}' does not exist.");