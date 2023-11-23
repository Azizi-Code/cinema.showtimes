using System.Collections.Immutable;
using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Application.Constants;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Exceptions;
using Cinema.Showtimes.Api.Domain.Repositories;
using Cinema.Showtimes.Api.Tests.TestHelpers;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;
using static Cinema.Showtimes.Api.Tests.Application.Commands.CreateReservationCommandHandlerTestHarness;

namespace Cinema.Showtimes.Api.Tests.Application.Commands;

public class CreateReservationCommandHandler_Handle
{
    [Fact]
    public async void ShowTimeNotExist_ThrowsException()
    {
        var showTimesRepositoryMock = Substitute.For<IShowtimesRepository>();
        showTimesRepositoryMock.GetWithTicketsAndMovieByIdAsync(DefaultShowTimeId, default).ReturnsNull();
        var sut = CreateSut(showtimesRepository: showTimesRepositoryMock);

        Func<Task> act = async () => await sut.Handle(DefaultReservationCommand, default);

        var exception = await Assert.ThrowsAsync<ShowtimeNotFoundException>(act);
        Assert.Equal($"ShowTime with id '{DefaultShowTimeId}' does not exist.", exception.Message);
    }

    [Fact]
    public async void AuditoriumNotExist_ThrowsException()
    {
        var auditoriumRepositoryMock = Substitute.For<IAuditoriumsRepository>();
        auditoriumRepositoryMock.GetWithSeatsByIdAsync(DefaultAuditoriumId, default).ReturnsNull();
        var sut = CreateSut(auditoriumsRepository: auditoriumRepositoryMock);

        Func<Task> act = async () => await sut.Handle(DefaultReservationCommand, default);

        var exception = await Assert.ThrowsAsync<AuditoriumNotFoundException>(act);
        Assert.Equal($"Auditorium with id '{DefaultAuditoriumId}' does not exist.", exception.Message);
    }

    [Fact]
    public async void SeatsAreNotValidForSelectedAuditorium_ThrowsException()
    {
        var auditoriumRepositoryMock = Substitute.For<IAuditoriumsRepository>();
        var differentAuditorium = new AuditoriumEntity(2, DefaultSeats);
        auditoriumRepositoryMock.GetWithSeatsByIdAsync(DefaultAuditoriumId, default).Returns(differentAuditorium);
        var sut = CreateSut(auditoriumsRepository: auditoriumRepositoryMock);

        Func<Task> act = async () => await sut.Handle(DefaultReservationCommand, default);

        var exception = await Assert.ThrowsAsync<AuditoriumAndShowTimeAuditoriumNotMatchedException>(act);
        Assert.Equal($"Auditorium with id '{DefaultAuditoriumId}' does not matched with showtime auditorium.",
            exception.Message);
    }

    [Fact]
    public async void SelectedSeatsAreNotExistInAuditorium_ThrowsException()
    {
        var reservationCommandWithInvalidSeat = new CreateReservationCommand(DefaultShowTimeId,
            ImmutableList.Create(new[] { new SeatEntity(DefaultAuditoriumId, DefaultRow, 2) }));
        var sut = CreateSut();

        Func<Task> act = async () => await sut.Handle(reservationCommandWithInvalidSeat, default);

        var exception = await Assert.ThrowsAsync<InvalidSeatsException>(act);
        Assert.Equal("The selected seats are not valid.",
            exception.Message);
    }

    [Fact]
    public async void ValidCommand_ReturnsReservedTicketResponse()
    {
        var sut = CreateSut();

        var result = await sut.Handle(DefaultReservationCommand, default);

        Assert.Equal(DefaultShowtime.Movie.Title, result.MovieTitle);
        Assert.Equal(DefaultTicket.Id, result.ReservationId);
        Assert.Collection(result.Seats.Seats,
            seat =>
            {
                Assert.Equal(DefaultSeats[0].SeatNumber, seat.SeatNumber);
                Assert.Equal(DefaultSeats[0].Row, seat.Row);
            });
    }
}

public static class CreateReservationCommandHandlerTestHarness
{
    public const int DefaultShowTimeId = 1;
    public const int DefaultAuditoriumId = 1;
    public const short DefaultRow = 1;
    public const short DefaultSeatNumber = 1;

    public static readonly SeatEntity DefaultSeatEntity = new(DefaultAuditoriumId, DefaultRow, DefaultSeatNumber);
    public static readonly ShowtimeEntity DefaultShowtime = ShowTimeBuilder.Create().Build();
    public static readonly ImmutableList<SeatEntity> DefaultSeats = ImmutableList.Create(new[] { DefaultSeatEntity });

    public static readonly AuditoriumEntity DefaultAuditorium =
        new(DefaultAuditoriumId, seats: DefaultSeats, showtimes: new List<ShowtimeEntity> { DefaultShowtime });

    public static readonly TicketEntity DefaultTicket =
        TicketEntityBuilder.Create().WithShowTime(DefaultShowtime).Build();

    public static readonly CreateReservationCommand DefaultReservationCommand = new(DefaultShowTimeId, DefaultSeats);

    public static CreateReservationCommandHandler CreateSut(IShowtimesRepository? showtimesRepository = null,
        ITicketsRepository? ticketsRepository = null, IAuditoriumsRepository? auditoriumsRepository = null) =>
        new(showtimesRepository ?? CreateShowTimeRepositoryMock(),
            ticketsRepository ?? CreateTicketRepositoryMock(),
            auditoriumsRepository ?? CreateAuditoriumRepositoryMock(),
            CreateConfiguration());

    private static IConfiguration CreateConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
                { { ApplicationConstant.ReservationTimeoutKey, "10" } }!)
            .Build();
    }

    private static IAuditoriumsRepository CreateAuditoriumRepositoryMock()
    {
        var auditoriumRepositoryMock = Substitute.For<IAuditoriumsRepository>();
        auditoriumRepositoryMock.GetWithSeatsByIdAsync(DefaultShowTimeId, default).Returns(DefaultAuditorium);
        return auditoriumRepositoryMock;
    }

    private static ITicketsRepository CreateTicketRepositoryMock()
    {
        var ticketRepositoryMock = Substitute.For<ITicketsRepository>();
        ticketRepositoryMock.CreateAsync(Arg.Any<ShowtimeEntity>(), Arg.Any<List<SeatEntity>>(), default)
            .Returns(DefaultTicket);
        return ticketRepositoryMock;
    }

    private static IShowtimesRepository CreateShowTimeRepositoryMock()
    {
        var showTimesRepositoryMock = Substitute.For<IShowtimesRepository>();
        showTimesRepositoryMock.GetWithTicketsAndMovieByIdAsync(DefaultShowTimeId, default).Returns(DefaultShowtime);
        return showTimesRepositoryMock;
    }
}