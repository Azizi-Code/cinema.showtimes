namespace Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}