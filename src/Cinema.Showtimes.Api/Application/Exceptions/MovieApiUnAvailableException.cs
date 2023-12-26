using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class MovieApiUnAvailableException(string message) : UnAvailableServiceException(message);