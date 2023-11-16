namespace Cinema.Showtimes.Api.Application.Exceptions.BaseExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}