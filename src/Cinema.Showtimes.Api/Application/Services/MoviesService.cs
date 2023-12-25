using Cinema.Showtimes.Api.Application.Caching;
using Cinema.Showtimes.Api.Application.Clients;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Application.Mappers;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using ProtoDefinitions;

namespace Cinema.Showtimes.Api.Application.Services;

public class MoviesService(IMoviesApiClient moviesApiClient, ICacheService cacheService)
    : IMoviesService
{
    private readonly IMoviesApiClient _moviesApiClient =
        Throw.ArgumentNullException.IfNull(moviesApiClient, nameof(moviesApiClient));

    private readonly ICacheService _cacheService =
        Throw.ArgumentNullException.IfNull(cacheService, nameof(cacheService));

    public async Task<ShowResponse?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var movie = await _moviesApiClient.GetByIdAsync(id, cancellationToken);
            if (movie != null)
            {
                await _cacheService.SetAsync(id, movie);
                return movie.MapToResponse();
            }

            throw new MovieNotFoundException(id);
        }
        catch (MovieApiUnAvailableException)
        {
            try
            {
                var movie = await _cacheService.GetAsync<showResponse>(id);

                return movie?.MapToResponse() ?? throw new MovieNotFoundException(id);
            }
            catch
            {
                throw new MovieApiUnAvailableException(id);
            }
        }
    }
}