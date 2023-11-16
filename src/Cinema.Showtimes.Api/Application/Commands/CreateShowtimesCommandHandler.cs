using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Common.BaseExceptions;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateShowtimesCommandHandler : IRequestHandler<CreateShowtimesCommand>
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


    public async Task Handle(CreateShowtimesCommand request, CancellationToken cancellationToken)
    {
        var existedShowtime = await _showtimesRepository.GetAllAsync(x =>
            x.AuditoriumId == request.AuditoriumId && x.SessionDate == request.SessionDate, cancellationToken);
        if (existedShowtime != null && existedShowtime.Any())
            throw new ShowtimeAlreadyExistException("This show already existed.");

        var auditorium = await _auditoriumsRepository.GetByIdAsync(request.AuditoriumId, cancellationToken);
        if (auditorium == null)
            throw new NotFoundException($"Auditorium with ID {request.AuditoriumId} not found.");

        var movie = await _moviesRepository.GetByIdAsync(request.MovieId, cancellationToken);
        if (movie == null)
            throw new NotFoundException($"Movie with ID {request.MovieId} not found");

        var showtime = ShowtimeEntity.Create(movie, request.SessionDate, auditorium.Id);
        await _showtimesRepository.CreateShowtimeAsync(showtime, cancellationToken);
    }
}