using Cinema.Showtimes.Api.Application.Dtos;

namespace Cinema.Showtimes.Api.Application.Responses;

public class ReservedTicketResponse(Guid reservationId, SeatsDto seats, string movieTitle)
{
    public Guid ReservationId { get; } = reservationId;
    public SeatsDto Seats { get; } = seats;
    public string MovieTitle { get; } = movieTitle;
}