using Cinema.Showtimes.Api.Common.BaseExceptions;

namespace Cinema.Showtimes.Api.Application.Exceptions;

public class TicketNotFoundException(string id) : NotFoundException($"Ticket not found: Reservation ID '{id}'.");