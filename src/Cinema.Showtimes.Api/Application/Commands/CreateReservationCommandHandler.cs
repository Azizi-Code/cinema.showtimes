using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Repositories;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
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

    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var showtime = await _showtimesRepository.GetWithTicketsByIdAsync(request.ShowtimeId, cancellationToken);
        if (showtime == null)
            throw new Exception("showtime is not valid");

        var auditorium =
            await _auditoriumsRepository.GetWithSeatsByIdAsync(request.SelectedSeats.First().AuditoriumId,
                cancellationToken);
        if (auditorium == null)
            throw new Exception("auditorium is not valid");

        if (showtime.AuditoriumId != auditorium.Id)
            throw new Exception("auditorium is not valid");

        var selectedSeats = request.SelectedSeats
            .Select(seat => auditorium.Seats.FirstOrDefault(x =>
                x.AuditoriumId == seat.AuditoriumId && x.SeatNumber == seat.SeatNumber && x.Row == seat.Row))
            .Where(res => res != null)?.ToList();

        if (selectedSeats == null || selectedSeats.Count != request.SelectedSeats.Count)
            throw new Exception("seats are not valid");

        TicketEntity.ReserveSeats(showtime, selectedSeats);

        var ticket = await _ticketsRepository.CreateAsync(showtime, selectedSeats, cancellationToken);
    }
}