namespace Cinema.Showtimes.Api.Application.Exceptions;

public class MovieNotFoundException : NotFoundException
{
    public MovieNotFoundException(string id) : base($"Movie with id '{id}' does not exist.")
    {
    }
}