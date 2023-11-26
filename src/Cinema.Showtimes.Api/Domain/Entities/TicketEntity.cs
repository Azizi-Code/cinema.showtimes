using Cinema.Showtimes.Api.Domain.Exceptions;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;

namespace Cinema.Showtimes.Api.Domain.Entities;

public class TicketEntity
{
    public Guid Id { get; private set; }
    public int ShowtimeId { get; private set; }
    public ICollection<SeatEntity> Seats { get; private set; }
    public DateTime CreatedTime { get; private set; }
    public bool Paid { get; private set; }
    public ShowtimeEntity Showtime { get; private set; }

    private TicketEntity()
    {
    }

    public TicketEntity(ShowtimeEntity showtime, ICollection<SeatEntity> seats)
    {
        Showtime = Throw.ArgumentNullException.IfNull(showtime, nameof(showtime));
        Seats = Throw.ArgumentNullException.IfNull(seats, nameof(seats));
        CreatedTime = DateTime.UtcNow;
    }

    public TicketEntity(Guid id, int showtimeId, ICollection<SeatEntity> seats, bool paid,
        ShowtimeEntity showtime, DateTime createdTime = new())
    {
        Id = Throw.ArgumentNullException.IfNull(id, nameof(id));
        ShowtimeId = showtimeId;
        Seats = Throw.ArgumentNullException.IfNull(seats, nameof(seats));
        Paid = paid;
        Showtime = showtime;
        CreatedTime = createdTime;
    }

    public void CheckSeatsAreAvailableForReservation(DateTime reservationTimeout)
    {
        CheckSelectedSeatsAreContiguous();
        CheckSelectedSeatsAreNotSoldOut();
        CheckSelectedSeatsAreNotAlreadyReserved(reservationTimeout);
    }

    private void CheckSelectedSeatsAreContiguous()
    {
        var sortedSeats = Seats.OrderBy(x => x.Row).ThenBy(x => x.SeatNumber).ToList();

        for (int i = 0; i < sortedSeats.Count - 1; i++)
        {
            if (sortedSeats[i].Row == sortedSeats[i + 1].Row &&
                sortedSeats[i].SeatNumber != sortedSeats[i + 1].SeatNumber - 1)
                throw new NotContiguousSeatsException();
        }
    }

    private void CheckSelectedSeatsAreNotSoldOut()
    {
        var soldOutTickets = Showtime.Tickets?.Where(x => x.Paid).ToList();
        if (soldOutTickets != null && soldOutTickets.Any())
        {
            var result = CheckSeatsAreAvailable(soldOutTickets);
            if (result) throw new SeatsSoldOutException();
        }
    }

    private void CheckSelectedSeatsAreNotAlreadyReserved(DateTime reservationTimeout)
    {
        var reservedTickets = Showtime.Tickets
            ?.Where(ticket => !ticket.Paid && ticket.CreatedTime >= reservationTimeout).ToList();
        if (reservedTickets != null && reservedTickets.Any())
        {
            var result = CheckSeatsAreAvailable(reservedTickets);
            if (result) throw new SeatsAlreadyReservedException();
        }
    }

    private bool CheckSeatsAreAvailable(IEnumerable<TicketEntity> tickets)
    {
        var seats = tickets.SelectMany(x => x.Seats);
        var seatsHashSet = seats.Select(x => (x.Row, x.SeatNumber)).ToHashSet();
        return Seats.Any(seat => seatsHashSet.Contains((seat.Row, seat.SeatNumber)));
    }

    public TicketEntity ConfirmPayment()
    {
        if (Paid) throw new ReservationAlreadyPaidException();

        Paid = true;
        return this;
    }
}