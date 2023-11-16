namespace Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

public class UnAvailableServiceException : Exception
{
    public UnAvailableServiceException(string message) : base(message)
    {
    }
}