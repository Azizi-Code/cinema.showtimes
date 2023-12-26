using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class AuditoriumsRepository(CinemaContext context) : IAuditoriumsRepository
{
    public async Task<AuditoriumEntity?> GetWithSeatsByIdAsync(int auditoriumId, CancellationToken cancellationToken)
    {
        return await context.Auditoriums
            .Include(x => x.Seats)
            .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancellationToken);
    }

    public async Task<AuditoriumEntity?> GetByIdAsync(int auditoriumId, CancellationToken cancellationToken)
    {
        return await context.Auditoriums
            .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancellationToken);
    }
}