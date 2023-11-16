using Cinema.Showtimes.Api.Application.Dtos;

namespace Cinema.Showtimes.Api.Application.Requests;

public class CreateReservationRequest
{
    public int ShowtimeId { get; }
    public IList<SeatEntityDto> SelectedSeats { get; }

    public CreateReservationRequest(int showtimeId, IList<SeatEntityDto> selectedSeats)
    {
        ShowtimeId = showtimeId;
        SelectedSeats = selectedSeats;
    }
}