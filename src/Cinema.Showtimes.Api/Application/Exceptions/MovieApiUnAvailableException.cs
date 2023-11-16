using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class MovieApiUnAvailableException : UnAvailableServiceException
{
    public MovieApiUnAvailableException(string message) : base(message)
    {
    }
}