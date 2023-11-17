using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Tests.TestHelpers;

public class ShowTimeBuilder
{
    private readonly int _id = 1;
    private readonly MovieEntity _movie = new(1, "", "", "", DateTime.Now);
    private readonly DateTime _sessionDate = new(2023, 10, 10, 10, 10, 10);
    private readonly int _auditoriumId = 1;
    private readonly ICollection<TicketEntity> _tickets;

    private ShowTimeBuilder()
    {
    }

    private ShowTimeBuilder(int id, MovieEntity movie, DateTime sessionDate, int auditoriumId,
        ICollection<TicketEntity> tickets)
    {
        _id = id;
        _movie = movie;
        _sessionDate = sessionDate;
        _auditoriumId = auditoriumId;
        _tickets = tickets;
    }

    public ShowtimeEntity Build() => new(_id, _movie, _sessionDate, _auditoriumId, _tickets);


    public static ShowTimeBuilder Create() => new();

    public ShowTimeBuilder WithTickets(ICollection<TicketEntity> tickets) =>
        new(_id, _movie, _sessionDate, _auditoriumId, tickets);
}