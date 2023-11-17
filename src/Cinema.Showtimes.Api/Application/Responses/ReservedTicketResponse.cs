using Cinema.Showtimes.Api.Application.Dtos;

namespace Cinema.Showtimes.Api.Application.Responses;

public class ReservedTicketResponse
{
    public Guid ReservationId { get; }
    public SeatsDto Seats { get; }
    public string MovieTitle { get; }

    public ReservedTicketResponse(Guid reservationId, SeatsDto seats, string movieTitle)
    {
        ReservationId = reservationId;
        Seats = seats;
        MovieTitle = movieTitle;
    }
}