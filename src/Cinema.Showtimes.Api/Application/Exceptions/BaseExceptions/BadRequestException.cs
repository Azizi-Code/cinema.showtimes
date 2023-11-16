namespace Cinema.Showtimes.Api.Application.Exceptions.BaseExceptions;

public class BadRequestException : Exception
{
    public string Target { get; }

    public BadRequestException(string message, string target) : base(message) => Target = target;
}