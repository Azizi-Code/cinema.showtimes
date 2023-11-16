namespace Cinema.Showtimes.Api.Common.BaseExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}