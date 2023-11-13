using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class MoviesRepository : IMoviesRepository
{
    private readonly CinemaContext _context;

    public MoviesRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<MovieEntity?> GetByIdAsync(int movieId, CancellationToken cancellationToken)
    {
        return await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == movieId, cancellationToken);
    }
}