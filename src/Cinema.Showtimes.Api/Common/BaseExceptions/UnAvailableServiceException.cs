namespace Cinema.Showtimes.Api.Common.BaseExceptions;

public class UnAvailableServiceException : Exception
{
    public UnAvailableServiceException(string message) : base(message)
    {
    }
}