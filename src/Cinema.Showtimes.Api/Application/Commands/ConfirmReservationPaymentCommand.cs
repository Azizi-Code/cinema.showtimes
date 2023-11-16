using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class ConfirmReservationPaymentCommand : IRequest
{
    public ConfirmReservationPaymentCommand(Guid reservationId)
    {
        ReservationId = reservationId;
    }

    public Guid ReservationId { get; }
}