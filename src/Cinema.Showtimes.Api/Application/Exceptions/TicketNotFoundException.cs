using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class TicketNotFoundException : NotFoundException
{
    public TicketNotFoundException(string id) : base($"Ticket not found: Reservation ID '{id}'.")
    {
    }
}