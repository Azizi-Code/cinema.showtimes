using System.Linq.Expressions;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class ShowtimesRepository(CinemaContext context) : IShowtimesRepository
{
    public async Task<ShowtimeEntity?> GetWithMoviesByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.ShowTimes
            .Include(x => x.Movie)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ShowtimeEntity?> GetWithTicketsAndMovieByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.ShowTimes
            .Include(x => x.Tickets)
            .Include(x => x.Movie)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ShowtimeEntity>?> GetAllAsync(Expression<Func<ShowtimeEntity, bool>>? filter,
        CancellationToken cancellationToken)
    {
        if (filter == null)
        {
            return await context.ShowTimes
                .Include(x => x.Movie)
                .ToListAsync(cancellationToken);
        }

        return await context.ShowTimes
            .Include(x => x.Movie)
            .Where(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task<ShowtimeEntity> CreateShowtimeAsync(ShowtimeEntity showtimeEntity,
        CancellationToken cancellationToken)
    {
        var showtime = await context.ShowTimes.AddAsync(showtimeEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return showtime.Entity;
    }
}