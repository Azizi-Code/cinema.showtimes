using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Application.Requests;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateShowtimesCommandHandler : IRequestHandler<CreateShowtimesCommand, CreateShowtimeResponse>
{
    private readonly IShowtimesRepository _showtimesRepository;
    private readonly IAuditoriumsRepository _auditoriumsRepository;
    private readonly IMoviesRepository _moviesRepository;


    public CreateShowtimesCommandHandler(IShowtimesRepository showtimesRepository,
        IAuditoriumsRepository auditoriumsRepository, IMoviesRepository moviesRepository)
    {
        _showtimesRepository = Throw.ArgumentNullException.IfNull(showtimesRepository, nameof(showtimesRepository));
        _auditoriumsRepository =
            Throw.ArgumentNullException.IfNull(auditoriumsRepository, nameof(auditoriumsRepository));
        _moviesRepository = Throw.ArgumentNullException.IfNull(moviesRepository, nameof(moviesRepository));
    }


    public async Task<CreateShowtimeResponse> Handle(CreateShowtimesCommand request,
        CancellationToken cancellationToken)
    {
        await CheckShowTimeIsNotAlreadyExistAsync(request.AuditoriumId, request.SessionDate, cancellationToken);

        var auditorium = await GetAuditoriumAsync(request.AuditoriumId, cancellationToken);
        var movie = await GetMovieAsync(request.MovieId, cancellationToken);

        var showtime = ShowtimeEntity.Create(movie, request.SessionDate, auditorium.Id);

        var result = await _showtimesRepository.CreateShowtimeAsync(showtime, cancellationToken);
        return new CreateShowtimeResponse(result.Id);
    }

    private async Task<AuditoriumEntity> GetAuditoriumAsync(int auditoriumId, CancellationToken cancellationToken)
    {
        var auditorium = await _auditoriumsRepository.GetByIdAsync(auditoriumId, cancellationToken);
        return auditorium ?? throw new AuditoriumNotFoundException(auditoriumId);
    }

    private async Task<MovieEntity> GetMovieAsync(int movieId, CancellationToken cancellationToken)
    {
        var movie = await _moviesRepository.GetByIdAsync(movieId, cancellationToken);
        return movie ?? throw new MovieNotFoundException(movieId.ToString());
    }

    private async Task CheckShowTimeIsNotAlreadyExistAsync(int requestAuditoriumId, DateTime requestSessionDate,
        CancellationToken cancellationToken)
    {
        var existedShowtime = await _showtimesRepository.GetAllAsync(x =>
            x.AuditoriumId == requestAuditoriumId && x.SessionDate == requestSessionDate, cancellationToken);
        if (existedShowtime != null && existedShowtime.Any())
            throw new ShowtimeAlreadyExistException("This show already existed.");
    }
}