namespace Cinema.Showtimes.Api.Application.Exceptions.BaseExceptions;

public class UnAvailableServiceException : Exception
{
    public UnAvailableServiceException(string message) : base(message)
    {
    }
}