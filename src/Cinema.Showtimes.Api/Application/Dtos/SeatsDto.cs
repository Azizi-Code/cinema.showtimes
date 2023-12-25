using System.Collections.Immutable;

namespace Cinema.Showtimes.Api.Application.Dtos;

public sealed class SeatsDto(int auditoriumId, ImmutableList<SeatDto> seats)
{
    public int AuditoriumId { get; } = auditoriumId;
    public ImmutableList<SeatDto> Seats { get; } = seats;
}

public sealed class SeatDto(short row, short seatNumber)
{
    public short Row { get; } = row;
    public short SeatNumber { get; } = seatNumber;
}