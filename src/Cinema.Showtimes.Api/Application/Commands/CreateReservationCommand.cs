using Cinema.Showtimes.Api.Domain.Entities;
using Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class CreateReservationCommand : IRequest
{
    public int ShowtimeId { get; }
    public IList<SeatEntity> SelectedSeats { get; }

    public CreateReservationCommand(int showtimeId, List<SeatEntity> selectedSeats)
    {
        ShowtimeId = showtimeId;
        Throw.ArgumentNullException.IfNull(selectedSeats, nameof(selectedSeats));
        SelectedSeats = Throw.ArgumentException.IfEmpty(selectedSeats, nameof(selectedSeats));
    }
}