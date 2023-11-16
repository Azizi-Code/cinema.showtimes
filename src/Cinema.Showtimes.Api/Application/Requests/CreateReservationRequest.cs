using Cinema.Showtimes.Api.Application.Dtos;

namespace Cinema.Showtimes.Api.Application.Requests;

public class CreateReservationRequest
{
    public int ShowtimeId { get; }
    public SeatsDto SelectedSeats { get; }

    public CreateReservationRequest(int showtimeId, SeatsDto selectedSeats)
    {
        ShowtimeId = showtimeId;
        SelectedSeats = selectedSeats;
    }
}