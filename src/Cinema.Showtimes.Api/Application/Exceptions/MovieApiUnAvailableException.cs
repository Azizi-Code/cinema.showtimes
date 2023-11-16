using Cinema.Showtimes.Api.Application.Exceptions.BaseExceptions;
using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class MovieApiUnAvailableException : UnAvailableServiceException
{
    public MovieApiUnAvailableException(string message) : base(message)
    {
    }
}