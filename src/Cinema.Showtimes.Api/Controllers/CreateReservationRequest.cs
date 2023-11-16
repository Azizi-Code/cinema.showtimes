namespace Cinema.Showtimes.Api.Controllers;

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

public class SeatEntityDto
{
    public short Row { get; }
    public short SeatNumber { get; }
    public int AuditoriumId { get; }

    public SeatEntityDto(int auditoriumId, short row, short seatNumber)
    {
        Row = row;
        SeatNumber = seatNumber;
        AuditoriumId = auditoriumId;
    }
}