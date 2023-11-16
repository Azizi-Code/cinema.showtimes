using Cinema.Showtimes.Api.Application.Caching;
using Cinema.Showtimes.Api.Application.Clients;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using ProtoDefinitions;

namespace Cinema.Showtimes.Api.Application.Services;

public interface IMoviesService
{
    Task<showResponse?> GetByIdAsync(string id, CancellationToken cancellationToken);
}

public class MoviesService : IMoviesService
{
    private readonly IMoviesApiClient _moviesApiClient;
    private readonly ICacheService _cacheService;

    public MoviesService(IMoviesApiClient moviesApiClient, ICacheService cacheService)
    {
        _moviesApiClient = Throw.ArgumentNullException.IfNull(moviesApiClient, nameof(moviesApiClient));
        _cacheService = Throw.ArgumentNullException.IfNull(cacheService, nameof(cacheService));
    }

    public async Task<showResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var movie = await _moviesApiClient.GetByIdAsync(id, cancellationToken);
            if (movie != null)
            {
                await _cacheService.SetAsync(id, movie);
                return movie;
            }

            throw new MovieNotFoundException(id);
        }
        catch (MovieApiUnAvailableException)
        {
            var movie = await _cacheService.GetAsync<showResponse>(id);

            return movie ?? throw new MovieNotFoundException(id);
        }
    }
}