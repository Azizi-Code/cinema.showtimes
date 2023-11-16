namespace Cinema.Showtimes.Api.Application.Dtos;

public sealed class SeatEntityDto
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