using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Exceptions.BaseExceptions;
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
            x.AuditoriumId == request.AuditoriumId, cancellationToken);
        if (existedShowtime != null)
        {
            throw new Exception("this show already existed");
        }

        var auditorium = await _auditoriumsRepository.GetByIdAsync(request.AuditoriumId, cancellationToken);
        if (auditorium == null)
            throw new NotFoundException("auditorium is not found");

        var movie = await _moviesRepository.GetByIdAsync(request.MovieId, cancellationToken);
        if (movie == null)
            throw new NotFoundException("movie is not found");

        var showtime = ShowtimeEntity.Create(movie, request.SessionDate, auditorium.Id);
        await _showtimesRepository.CreateShowtimeAsync(showtime, cancellationToken);
    }
}