using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Infrastructure.ActionResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Controllers;

[ApiController]
[Route("api/vi/payment")]
public class PaymentController : Controller
{
    private readonly IMediator _mediator;
    private readonly IActionResultMapper<ShowtimesController> _actionResultMapper;


    public PaymentController(IMediator mediator, IActionResultMapper<ShowtimesController> actionResultMapper)
    {
        _mediator = mediator;
        _actionResultMapper = actionResultMapper;
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmPayment(Guid reservationId, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new ConfirmReservationPaymentCommand(reservationId), cancellationToken);
        }
        catch (Exception exception)
        {
            return _actionResultMapper.Map(exception);
        }

        return Ok();
    }
}