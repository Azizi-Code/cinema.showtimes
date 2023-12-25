using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class ShowtimeAlreadyExistException(string message) : UnprocessableEntityException(message);