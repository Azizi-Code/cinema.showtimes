namespace Cinema.Showtimes.Api.Application.Exceptions.BaseExceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message) : base(message)
    {
    }
}