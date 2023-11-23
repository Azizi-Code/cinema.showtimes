using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Domain.Repositories;

public interface IMoviesRepository
{
    Task<MovieEntity?> GetByIdAsync(int movieId, CancellationToken cancellationToken);
}