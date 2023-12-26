using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class ConfirmReservationPaymentCommand(Guid reservationId) : IRequest
{
    public Guid ReservationId { get; } = reservationId;
}