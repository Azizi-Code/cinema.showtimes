namespace Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;

public class BadRequestException : Exception
{
    public string Target { get; }

    public BadRequestException(string message, string target) : base(message) => Target = target;
}