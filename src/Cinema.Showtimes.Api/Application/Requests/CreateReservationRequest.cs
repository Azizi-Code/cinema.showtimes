using Cinema.Showtimes.Api.Application.Dtos;

namespace Cinema.Showtimes.Api.Application.Requests;

public class CreateReservationRequest(int showtimeId, SeatsDto selectedSeats)
{
    public int ShowtimeId { get; } = showtimeId;
    public SeatsDto SelectedSeats { get; } = selectedSeats;
}