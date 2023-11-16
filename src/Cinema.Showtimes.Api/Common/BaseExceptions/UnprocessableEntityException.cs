namespace Cinema.Showtimes.Api.Common.BaseExceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message) : base(message)
    {
    }
}