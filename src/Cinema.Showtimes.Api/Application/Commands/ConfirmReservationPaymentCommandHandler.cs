using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Domain.Repositories;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class ConfirmReservationPaymentCommandHandler(ITicketsRepository ticketsRepository)
    : IRequestHandler<ConfirmReservationPaymentCommand>
{
    public async Task Handle(ConfirmReservationPaymentCommand request, CancellationToken cancellationToken)
    {
        var ticket = await ticketsRepository.GetAsync(request.ReservationId, cancellationToken);
        if (ticket == null)
            throw new TicketNotFoundException(request.ReservationId.ToString());

        var confirmedTicket = ticket.ConfirmPayment();
        await ticketsRepository.ConfirmPaymentAsync(confirmedTicket, cancellationToken);
    }
}