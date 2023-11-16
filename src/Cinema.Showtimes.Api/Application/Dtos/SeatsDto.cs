using System.Collections.Immutable;

namespace Cinema.Showtimes.Api.Application.Dtos;

public sealed class SeatsDto
{
    public int AuditoriumId { get; }
    public ImmutableList<SeatDto> Seats { get; }

    public SeatsDto(int auditoriumId, ImmutableList<SeatDto> seats)
    {
        AuditoriumId = auditoriumId;
        Seats = seats;
    }
}

public sealed class SeatDto
{
    public short Row { get; }
    public short SeatNumber { get; }

    public SeatDto(short row, short seatNumber)
    {
        Row = row;
        SeatNumber = seatNumber;
    }
}