using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Tests.TestHelpers;

public class TicketEntityBuilder
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly int _showtimeId = 1;
    private readonly ICollection<SeatEntity> _seats = new List<SeatEntity> { new(1, 1, 1) };
    private readonly DateTime _createdTime = new(2023, 10, 10, 10, 10, 10);
    private readonly bool _paid;
    private readonly ShowtimeEntity _showtime;

    private TicketEntityBuilder()
    {
    }

    private TicketEntityBuilder(Guid id, int showtimeId, ICollection<SeatEntity> seats, DateTime createdTime, bool paid,
        ShowtimeEntity showtime)
    {
        _id = id;
        _showtimeId = showtimeId;
        _seats = seats;
        _createdTime = createdTime;
        _paid = paid;
        _showtime = showtime;
    }

    public TicketEntityBuilder WithId(Guid id) => new(id, _showtimeId, _seats, _createdTime, _paid, _showtime);
    public TicketEntityBuilder WithPaid(bool paid) => new(_id, _showtimeId, _seats, _createdTime, paid, _showtime);

    public TicketEntity Build() => new(_id, _showtimeId, _seats, _paid, _createdTime);


    public static TicketEntityBuilder Create() => new();
}