﻿using Cinema.Showtimes.Api.Domain.Exceptions;

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
        Showtime = showtime;
        Seats = seats;
        CreatedTime = DateTime.UtcNow;
    }

    public TicketEntity(Guid id, int showtimeId, ICollection<SeatEntity> seats, bool paid, DateTime createdTime = new())
    {
        Id = id;
        ShowtimeId = showtimeId;
        Seats = seats;
        Paid = paid;
        CreatedTime = createdTime;
    }

    public static void ReserveSeats(ShowtimeEntity showtime, ICollection<SeatEntity> selectedSeats,
        DateTime reservationTimeout)
    {
        CheckSelectedSeatsAreContiguous(selectedSeats);

        var soldOutTickets = showtime.Tickets?.Where(x => x.Paid).ToList();
        if (soldOutTickets != null && soldOutTickets.Any())
        {
            var soldOutSeats = soldOutTickets.SelectMany(x => x.Seats);
            CheckSelectedSeatsAreNotSoldOut(selectedSeats, soldOutSeats);
        }

        var reservedTicketLessThanReservationTimeout =
            showtime.Tickets?.Where(ticket => !ticket.Paid && ticket.CreatedTime >= reservationTimeout).ToList();
        if (reservedTicketLessThanReservationTimeout != null && reservedTicketLessThanReservationTimeout.Any())
        {
            var reservedSeatsLessThanReservationTimeout =
                reservedTicketLessThanReservationTimeout.SelectMany(ticket => ticket.Seats);

            CheckSelectedSeatsAreNotAlreadyReserved(selectedSeats, reservedSeatsLessThanReservationTimeout);
        }
    }

    private static void CheckSelectedSeatsAreContiguous(IEnumerable<SeatEntity> selectedSeats)
    {
        var sortedSeats = selectedSeats.OrderBy(x => x.Row).ThenBy(x => x.SeatNumber).ToList();

        for (int i = 0; i < sortedSeats.Count - 1; i++)
        {
            if (sortedSeats[i].Row == sortedSeats[i + 1].Row &&
                sortedSeats[i].SeatNumber != sortedSeats[i + 1].SeatNumber - 1)
                throw new NotContiguousSeatsException();
        }
    }

    private static void CheckSelectedSeatsAreNotAlreadyReserved(IEnumerable<SeatEntity> selectedSeats,
        IEnumerable<SeatEntity> reservedSeatsLessThanTimeout)
    {
        var reservedSeatsHash = reservedSeatsLessThanTimeout.Select(seat => (seat.Row, seat.SeatNumber)).ToHashSet();
        var result = selectedSeats.Any(seat => reservedSeatsHash.Contains((seat.Row, seat.SeatNumber)));

        if (result) throw new SeatsAlreadyReservedException();
    }

    private static void CheckSelectedSeatsAreNotSoldOut(IEnumerable<SeatEntity> selectedSeats,
        IEnumerable<SeatEntity> unavailableSeats)
    {
        var unavailableSeatsHashSet = unavailableSeats.Select(x => (x.Row, x.SeatNumber)).ToHashSet();
        var result = selectedSeats.Any(seat => unavailableSeatsHashSet.Contains((seat.Row, seat.SeatNumber)));

        if (result) throw new SeatsSoldOutException();
    }

    public TicketEntity ConfirmPayment()
    {
        if (Paid) throw new ReservationAlreadyPaidException();

        Paid = true;
        return this;
    }
}