namespace Cinema.Showtimes.Api.Application.Dtos;

public class ReservedTicketResponse
{
    //Reserving the seat response will contain a GUID of the reservation, also the number of seats,
    //the auditorium used and the movie that will be played.

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