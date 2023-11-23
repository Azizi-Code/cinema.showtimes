using System.Collections.Immutable;
using Cinema.Showtimes.Api.Application.Dtos;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Domain.Entities;

namespace Cinema.Showtimes.Api.Application.Mappers;

public static class TicketEntityMapper
{
    public static ReservedTicketResponse MapToReservedTicketResponse(this TicketEntity ticket) =>
        new(ticket.Id, ticket.Seats.MapToDto(), ticket.Showtime.Movie.Title);
}

public static class SeatsMapper
{
    public static ImmutableList<SeatEntity> MapToEntity(this SeatsDto seats) => seats.Seats
        .Select(seat => new SeatEntity(seats.AuditoriumId, seat.Row, seat.SeatNumber)).ToImmutableList();

    public static SeatsDto MapToDto(this ICollection<SeatEntity> seats) =>
        new(seats.FirstOrDefault().AuditoriumId,
            seats.Select(x => new SeatDto(x.Row, x.SeatNumber)).ToImmutableList());
}