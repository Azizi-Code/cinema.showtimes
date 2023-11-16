namespace Cinema.Showtimes.Api.Domain.Entities;

public class SeatEntity
{
    public short Row { get; private set; }
    public short SeatNumber { get; private set; }
    public int AuditoriumId { get; private set; }
    public AuditoriumEntity Auditorium { get; private set; }

    private SeatEntity()
    {
    }

    public SeatEntity(int auditoriumId, short row, short seatNumber)
    {
        Row = row;
        SeatNumber = seatNumber;
        AuditoriumId = auditoriumId;
    }
}