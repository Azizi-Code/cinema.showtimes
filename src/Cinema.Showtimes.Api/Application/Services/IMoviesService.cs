using Cinema.Showtimes.Api.Application.Responses;

namespace Cinema.Showtimes.Api.Application.Services;

public interface IMoviesService
{
    Task<ShowResponse?> GetByIdAsync(string id, CancellationToken cancellationToken);
}