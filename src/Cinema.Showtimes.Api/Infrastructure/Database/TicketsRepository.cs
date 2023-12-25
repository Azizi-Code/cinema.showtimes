using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class TicketsRepository(CinemaContext context) : ITicketsRepository
{
    public Task<TicketEntity?> GetAsync(Guid id, CancellationToken cancellationToken) =>
        context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<IEnumerable<TicketEntity>> GetEnrichedAsync(int showtimeId, CancellationToken cancellationToken)
    {
        return await context.Tickets
            .Include(x => x.Showtime)
            .Include(x => x.Seats)
            .Where(x => x.ShowtimeId == showtimeId)
            .ToListAsync(cancellationToken);
    }

    public async Task<TicketEntity> CreateAsync(ShowtimeEntity showtime, IEnumerable<SeatEntity> selectedSeats,
        CancellationToken cancellationToken)
    {
        var ticket = context.Tickets.Add(new TicketEntity(showtime, new List<SeatEntity>(selectedSeats)));
        await context.SaveChangesAsync(cancellationToken);

        return ticket.Entity;
    }

    public async Task<TicketEntity> ConfirmPaymentAsync(TicketEntity ticket, CancellationToken cancellationToken)
    {
        context.Update(ticket);
        await context.SaveChangesAsync(cancellationToken);

        return ticket;
    }
}