using Cinema.Showtimes.Api.Application.Constants;
using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Application.Mappers;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Domain.Exceptions;
using Cinema.Showtimes.Api.Domain.Repositories;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateReservationCommandHandler(
    IShowtimesRepository showtimesRepository,
    ITicketsRepository ticketsRepository,
    IAuditoriumsRepository auditoriumsRepository,
    IConfiguration configuration)
    : IRequestHandler<CreateReservationCommand, ReservedTicketResponse>
{
    public async Task<ReservedTicketResponse> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var showtime = await GetShowTimeWithTicketsAndMovieAsync(request.ShowtimeId, cancellationToken);

        var requestAuditoriumId = request.SelectedSeats.FirstOrDefault().AuditoriumId;
        var auditorium =
            await GetAuditoriumWithSeatsAsync(requestAuditoriumId, showtime.AuditoriumId, cancellationToken);

        var selectedSeats = CheckSelectedSeatsAreExistInAuditorium(request.SelectedSeats, auditorium);
        var reservationTimout = GetReservationTimout();

        var ticket = new TicketEntity(showtime, selectedSeats);
        ticket.CheckSeatsAreAvailableForReservation(reservationTimout);

        var reservedTicket = await ticketsRepository.CreateAsync(ticket.Showtime, ticket.Seats, cancellationToken);

        return reservedTicket.MapToReservedTicketResponse();
    }

    private async Task<ShowtimeEntity> GetShowTimeWithTicketsAndMovieAsync(int showtimeId,
        CancellationToken cancellationToken)
    {
        var showtime = await showtimesRepository.GetWithTicketsAndMovieByIdAsync(showtimeId, cancellationToken);
        return showtime ?? throw new ShowtimeNotFoundException(showtimeId);
    }

    private async Task<AuditoriumEntity> GetAuditoriumWithSeatsAsync(int requestAuditoriumId, int showtimeAuditoriumId,
        CancellationToken cancellationToken)
    {
        var auditorium = await auditoriumsRepository.GetWithSeatsByIdAsync(requestAuditoriumId, cancellationToken);

        if (auditorium == null)
            throw new AuditoriumNotFoundException(requestAuditoriumId);

        if (showtimeAuditoriumId != auditorium?.Id)
            throw new AuditoriumAndShowTimeAuditoriumNotMatchedException(requestAuditoriumId);

        return auditorium;
    }

    private IList<SeatEntity> CheckSelectedSeatsAreExistInAuditorium(IList<SeatEntity> selectedSeats,
        AuditoriumEntity auditorium)
    {
        var seats = selectedSeats
            .Select(seat => auditorium.Seats.FirstOrDefault(x =>
                x.AuditoriumId == seat.AuditoriumId && x.SeatNumber == seat.SeatNumber && x.Row == seat.Row))
            .Where(res => res != null)?.ToList();

        if (seats == null || seats.Count != selectedSeats.Count)
            throw new InvalidSeatsException();

        return seats;
    }

    private DateTime GetReservationTimout()
    {
        var timeout = configuration.GetValue<int>(ApplicationConstant.ReservationTimeoutKey);
        return DateTime.UtcNow.AddMinutes(-timeout);
    }
}