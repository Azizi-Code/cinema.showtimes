using Cinema.Showtimes.Api.Application.Exceptions;
using Cinema.Showtimes.Api.Domain.Repositories;
using MediatR;

namespace Cinema.Showtimes.Api.Application.Commands;

public class ConfirmReservationPaymentCommandHandler : IRequestHandler<ConfirmReservationPaymentCommand>
{
    private readonly ITicketsRepository _ticketsRepository;

    public ConfirmReservationPaymentCommandHandler(ITicketsRepository ticketsRepository) =>
        _ticketsRepository = ticketsRepository;

    public async Task Handle(ConfirmReservationPaymentCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketsRepository.GetAsync(request.ReservationId, cancellationToken);
        if (ticket == null)
            throw new TicketNotFoundException(request.ReservationId.ToString());

        var confirmedTicket = ticket.ConfirmPayment();
        await _ticketsRepository.ConfirmPaymentAsync(confirmedTicket, cancellationToken);
    }
}