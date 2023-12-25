using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class MoviesRepository(CinemaContext context) : IMoviesRepository
{
    public async Task<MovieEntity?> GetByIdAsync(int movieId, CancellationToken cancellationToken)
    {
        return await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId, cancellationToken);
    }
}