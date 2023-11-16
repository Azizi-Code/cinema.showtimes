using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Showtimes.Api.Infrastructure.Database;

public class TicketsRepository : ITicketsRepository
{
    private readonly CinemaContext _context;

    public TicketsRepository(CinemaContext context)
    {
        _context = context;
    }

    public Task<TicketEntity> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TicketEntity>> GetEnrichedAsync(int showtimeId, CancellationToken cancellationToken)
    {
        return await _context.Tickets
            .Include(x => x.Showtime)
            .Include(x => x.Seats)
            .Where(x => x.ShowtimeId == showtimeId)
            .ToListAsync(cancellationToken);
    }

    public async Task<TicketEntity> CreateAsync(ShowtimeEntity showtime, IEnumerable<SeatEntity> selectedSeats,
        CancellationToken cancellationToken)
    {
        var ticket = _context.Tickets.Add(new TicketEntity(showtime, new List<SeatEntity>(selectedSeats)));
        await _context.SaveChangesAsync(cancellationToken);

        return ticket.Entity;
    }

    public async Task<TicketEntity> CreateAsync(TicketEntity ticketEntity, CancellationToken cancel)
    {
        var ticket = _context.Tickets.Add(ticketEntity);
        await _context.SaveChangesAsync(cancel);
        return ticket.Entity;
    }

    public async Task<TicketEntity> ConfirmPaymentAsync(TicketEntity ticket, CancellationToken cancellationToken)
    {
        ticket = ticket.ConfirmPaymentAsync();
        _context.Update(ticket);
        await _context.SaveChangesAsync(cancellationToken);
        return ticket;
    }
}