using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Exceptions;
using Cinema.Showtimes.Api.Tests.TestHelpers;
using Xunit;
using static Cinema.Showtimes.Api.Tests.Domain.Entities.TicketEntityTestHarness;

namespace Cinema.Showtimes.Api.Tests.Domain.Entities;

public class TicketEntity_ReserveSeats
{
    [Fact]
    public void NonContiguousSeats_ThrowsException()
    {
        var nonContiguousSeats = new List<SeatEntity>
            { new(DefaultAuditoriumId, 1, 1), new(DefaultAuditoriumId, 1, 4) };
        var ticket = new TicketEntity(DefaultShowtime, nonContiguousSeats);

        var result = Record.Exception(() => ticket.ReserveSeats(DateTime.UtcNow));

        Assert.IsType<NotContiguousSeatsException>(result);
    }

    [Fact]
    public void ReserveSoldOutSeats_ThrowsException()
    {
        var soldOutTickets = new[]
            { new TicketEntity(Guid.NewGuid(), DefaultShowTimeId, DefaultSeats, true, default!) };
        var showtime = ShowTimeBuilder.Create().WithTickets(soldOutTickets).Build();
        var ticket = new TicketEntity(showtime, DefaultSeats);

        var result = Record.Exception(() => ticket.ReserveSeats(DateTime.UtcNow));

        Assert.IsType<SeatsSoldOutException>(result);
    }

    [Fact]
    public void ReserveAlreadyReservedSeats_ThrowsException()
    {
        var reservedTickets = new[]
        {
            new TicketEntity(Guid.NewGuid(), DefaultShowTimeId, DefaultSeats, false, default!,
                createdTime: DateTime.UtcNow.AddSeconds(60))
        };
        var showtime = ShowTimeBuilder.Create().WithTickets(reservedTickets).Build();
        var ticket = new TicketEntity(showtime, DefaultSeats);

        var result = Record.Exception(() => ticket.ReserveSeats(DateTime.UtcNow));

        Assert.IsType<SeatsAlreadyReservedException>(result);
    }
}

public class TicketEntity_ConfirmPayment
{
    [Fact]
    public void ConfirmPayment_ReturnsWithTruePaid()
    {
        var ticket = TicketEntityBuilder.Create().Build();

        var result = ticket.ConfirmPayment();

        Assert.True(result.Paid);
    }

    [Fact]
    public void ConfirmAlreadyPaidPayment_ThrowException()
    {
        var ticket = TicketEntityBuilder.Create().WithPaid(true).Build();

        var result = Record.Exception(() => ticket.ConfirmPayment());

        Assert.IsType<ReservationAlreadyPaidException>(result);
    }
}

public static class TicketEntityTestHarness
{
    public const int DefaultAuditoriumId = 1;
    public const int DefaultShowTimeId = 1;

    public static readonly ICollection<SeatEntity> DefaultSeats = new List<SeatEntity>
    {
        new(DefaultAuditoriumId, 1, 1)
    };

    public static readonly ShowtimeEntity DefaultShowtime = ShowTimeBuilder.Create().Build();
}