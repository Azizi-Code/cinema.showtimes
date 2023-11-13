namespace Cinema.Showtimes.Api.Application.Exceptions;

public class MovieApiException : Exception
{
    public MovieApiException(string message) : base(message)
    {
    }
}