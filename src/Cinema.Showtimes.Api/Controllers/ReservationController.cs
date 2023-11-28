using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Application.Mappers;
using Cinema.Showtimes.Api.Application.Requests;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Infrastructure.ActionResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Controllers;

[ApiController]
[Route("api/v1/reservation")]
public class ReservationController : Controller
{
    private readonly IMediator _mediator;
    private readonly IActionResultMapper<ReservationController> _actionResultMapper;

    public ReservationController(IMediator mediator, IActionResultMapper<ReservationController> actionResultMapper)
    {
        _mediator = mediator;
        _actionResultMapper = actionResultMapper;
    }

    [HttpPost]
    public async Task<IActionResult> Reserve([FromBody] CreateReservationRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var createReservationCommand =
                new CreateReservationCommand(request.ShowtimeId, request.SelectedSeats.MapToEntity());
            var result = await _mediator.Send<ReservedTicketResponse>(createReservationCommand, cancellationToken);

            return Ok(result);
        }
        catch (Exception exception)
        {
            return _actionResultMapper.Map(exception);
        }
    }

    [HttpPut("Confirm")]
    public async Task<IActionResult> ConfirmReservation(Guid reservationId, CancellationToken cancellationToken)
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