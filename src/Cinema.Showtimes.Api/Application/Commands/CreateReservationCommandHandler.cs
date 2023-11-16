using Cinema.Showtimes.Api.Application.Dtos;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Application.Mappers;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Exceptions;
using Cinema.Showtimes.Api.Domain.Repositories;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservedTicketResponse>
{
    private readonly IShowtimesRepository _showtimesRepository;
    private readonly ITicketsRepository _ticketsRepository;
    private readonly IAuditoriumsRepository _auditoriumsRepository;

    public CreateReservationCommandHandler(IShowtimesRepository showtimesRepository,
        ITicketsRepository ticketsRepository, IAuditoriumsRepository auditoriumsRepository)
    {
        _showtimesRepository = showtimesRepository;
        _ticketsRepository = ticketsRepository;
        _auditoriumsRepository = auditoriumsRepository;
    }

    public async Task<ReservedTicketResponse> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var showtime =
            await _showtimesRepository.GetWithTicketsAndMovieByIdAsync(request.ShowtimeId, cancellationToken);
        if (showtime == null)
            throw new ShowtimeNotFound(request.ShowtimeId);

        var auditoriumId = request.SelectedSeats.FirstOrDefault().AuditoriumId;
        var auditorium = await _auditoriumsRepository.GetWithSeatsByIdAsync(auditoriumId, cancellationToken);
        if (auditorium == null)
            throw new AuditoriumNotFoundException(request.SelectedSeats.FirstOrDefault().AuditoriumId);

        if (showtime.AuditoriumId != auditoriumId)
            throw new InvalidAuditoriumException();

        var selectedSeats = request.SelectedSeats
            .Select(seat => auditorium.Seats.FirstOrDefault(x =>
                x.AuditoriumId == seat.AuditoriumId && x.SeatNumber == seat.SeatNumber && x.Row == seat.Row))
            .Where(res => res != null)?.ToList();

        if (selectedSeats == null || selectedSeats.Count != request.SelectedSeats.Count)
            throw new InvalidSeatsException();

        TicketEntity.ReserveSeats(showtime, selectedSeats);

        var reservedTicket = await _ticketsRepository.CreateAsync(showtime, selectedSeats, cancellationToken);

        return reservedTicket.MapToResponse();
    }
}