namespace Cinema.Showtimes.Api.Domain.Entities;

public class SeatEntity
{
    public short Row { get; }
    public short SeatNumber { get;  }
    public int AuditoriumId { get; }
    public AuditoriumEntity Auditorium { get;  }

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