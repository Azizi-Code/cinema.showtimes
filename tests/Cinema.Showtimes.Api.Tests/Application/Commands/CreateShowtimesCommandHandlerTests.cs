using System.Collections.Immutable;
using System.Linq.Expressions;
using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Cinema.Showtimes.Api.Tests.TestHelpers;
using NSubstitute;
using Xunit;
using static Cinema.Showtimes.Api.Tests.Application.Commands.CreateShowtimesCommandHandlerTestHarness;

namespace Cinema.Showtimes.Api.Tests.Application.Commands;

public class CreateShowtimesCommandHandler_Handle
{
    [Fact]
    public async void ShowTimeAlreadyExist_ThrowsException()
    {
        var showTimesRepositoryMock = Substitute.For<IShowtimesRepository>();
        showTimesRepositoryMock.GetAllAsync(Arg.Any<Expression<Func<ShowtimeEntity, bool>>>(), default)
            .Returns(new List<ShowtimeEntity> { DefaultShowtime });
        var sut = CreateSut(showtimesRepository: showTimesRepositoryMock);

        Func<Task> act = async () => await sut.Handle(DefaultCreateShowtimesCommand, default);

        var exception = await Assert.ThrowsAsync<ShowtimeAlreadyExistException>(act);
        Assert.Equal("This show already existed.", exception.Message);
    }

    [Fact]
    public async void AuditoriumDoesNotExist_ThrowsException()
    {
        var sut = CreateSut();

        Func<Task> act = async () => await sut.Handle(DefaultCreateShowtimesCommand, default);

        var exception = await Assert.ThrowsAsync<AuditoriumNotFoundException>(act);
        Assert.Equal($"Auditorium with id '{DefaultAuditoriumId}' does not exist.", exception.Message);
    }

    [Fact]
    public async void MovieDoesNotExist_ThrowsException()
    {
        var auditoriumsRepositoryMock = Substitute.For<IAuditoriumsRepository>();
        auditoriumsRepositoryMock.GetByIdAsync(Arg.Any<int>(), default).Returns(DefaultAuditorium);
        var sut = CreateSut(auditoriumsRepository: auditoriumsRepositoryMock);

        Func<Task> act = async () => await sut.Handle(DefaultCreateShowtimesCommand, default);

        var exception = await Assert.ThrowsAsync<MovieNotFoundException>(act);
        Assert.Equal($"Movie with id '{DefaultMovieId}' does not exist.", exception.Message);
    }

    [Fact]
    public async void ValidCreateShowtimesCommand_ReturnsCreateShowtimeResponse()
    {
        var auditoriumsRepositoryMock = Substitute.For<IAuditoriumsRepository>();
        auditoriumsRepositoryMock.GetByIdAsync(Arg.Any<int>(), default).Returns(DefaultAuditorium);
        var movieRepositoryMock = Substitute.For<IMoviesRepository>();
        movieRepositoryMock.GetByIdAsync(Arg.Any<int>(), default).Returns(DefaultMovie);
        var sut = CreateSut(auditoriumsRepository: auditoriumsRepositoryMock, moviesRepository: movieRepositoryMock);

        var result = await sut.Handle(DefaultCreateShowtimesCommand, default);

        Assert.Equal(DefaultShowtime.Id, result.ShowTimeId);
    }
}

public static class CreateShowtimesCommandHandlerTestHarness
{
    public const int DefaultMovieId = 1;
    public const int DefaultAuditoriumId = 1;

    public static readonly ShowtimeEntity DefaultShowtime = ShowTimeBuilder.Create().Build();

    public static readonly AuditoriumEntity DefaultAuditorium =
        new(DefaultAuditoriumId, ImmutableList.Create(new[] { new SeatEntity(DefaultAuditoriumId, 1, 1) }));

    public static readonly MovieEntity DefaultMovie =
        new(1, "DefaultTitle", "DefaultImdbId", "DefaultStars", new DateTime(2020, 1, 2));

    public static readonly CreateShowtimesCommand DefaultCreateShowtimesCommand =
        new(DefaultAuditoriumId, DefaultAuditoriumId, DateTime.UtcNow);

    public static CreateShowtimesCommandHandler CreateSut(IShowtimesRepository? showtimesRepository = null,
        IAuditoriumsRepository? auditoriumsRepository = null, IMoviesRepository? moviesRepository = null)
    {
        return new CreateShowtimesCommandHandler(showtimesRepository ?? CreateShowTimeRepositoryMock(),
            auditoriumsRepository ?? Substitute.For<IAuditoriumsRepository>(),
            moviesRepository ?? Substitute.For<IMoviesRepository>());
    }

    private static IShowtimesRepository CreateShowTimeRepositoryMock()
    {
        var showTimesRepositoryMock = Substitute.For<IShowtimesRepository>();
        showTimesRepositoryMock.CreateShowtimeAsync(Arg.Any<ShowtimeEntity>(), default)
            .Returns(DefaultShowtime);
        return showTimesRepositoryMock;
    }
}