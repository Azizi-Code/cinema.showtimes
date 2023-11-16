using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Domain.Repositories;

public interface ITicketsRepository
{
    Task<TicketEntity> ConfirmPaymentAsync(TicketEntity ticket, CancellationToken cancellationToken);

    Task<TicketEntity> CreateAsync(ShowtimeEntity showtime, IEnumerable<SeatEntity> selectedSeats,
        CancellationToken cancellationToken);

    Task<TicketEntity> GetAsync(Guid id, CancellationToken cancel);
    Task<IEnumerable<TicketEntity>> GetEnrichedAsync(int showtimeId, CancellationToken cancellationToken);
}