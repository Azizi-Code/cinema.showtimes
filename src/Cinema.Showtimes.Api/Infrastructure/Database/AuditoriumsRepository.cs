using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class AuditoriumsRepository : IAuditoriumsRepository
{
    private readonly CinemaContext _context;

    public AuditoriumsRepository(CinemaContext context)
    {
        _context = context;
    }

    public async Task<AuditoriumEntity?> GetWithSeatsByIdAsync(int auditoriumId, CancellationToken cancellationToken)
    {
        return await _context.Auditoriums
            .Include(x => x.Seats)
            .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancellationToken);
    }

    public async Task<AuditoriumEntity?> GetByIdAsync(int auditoriumId, CancellationToken cancellationToken)
    {
        return await _context.Auditoriums
            .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancellationToken);
    }
}