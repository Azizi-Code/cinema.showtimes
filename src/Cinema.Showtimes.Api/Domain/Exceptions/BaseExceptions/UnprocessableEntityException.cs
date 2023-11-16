namespace Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message) : base(message)
    {
    }
}