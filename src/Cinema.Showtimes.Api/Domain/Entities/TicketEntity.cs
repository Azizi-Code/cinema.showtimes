namespace Cinema.Showtimes.Api.Domain.Entities;

public class TicketEntity
{
    public Guid Id { get; }
    public int ShowtimeId { get; }
    public ICollection<SeatEntity> Seats { get; }
    public DateTime CreatedTime { get; } = DateTime.Now;
    public bool Paid { get; }
    public ShowtimeEntity Showtime { get; }

    private TicketEntity()
    {
    }

    private TicketEntity(ShowtimeEntity showtime, ICollection<SeatEntity> seats)
    {
        Showtime = showtime;
        Seats = seats;
    }

    public TicketEntity(Guid id, int showtimeId, ICollection<SeatEntity> seats, bool paid, ShowtimeEntity showtime)
    {
        Id = id;
        ShowtimeId = showtimeId;
        Seats = seats;
        Paid = paid;
        Showtime = showtime;
    }

    public static TicketEntity Create(ShowtimeEntity showtime, ICollection<SeatEntity> selectedSeats)
    {
        return new TicketEntity(showtime, selectedSeats);
    }

    public TicketEntity ConfirmPaymentAsync() => new(Id, ShowtimeId, Seats, true, Showtime);
}