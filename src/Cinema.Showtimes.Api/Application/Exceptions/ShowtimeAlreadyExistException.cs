using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class ShowtimeAlreadyExistException : UnprocessableEntityException
{
    public ShowtimeAlreadyExistException(string message) : base(message)
    {
    }
}