using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Controllers;

[ApiController]
[Route("api/v1/reservation")]
public class ReservationController : Controller
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Reserve([FromBody] CreateReservationRequest request)
    {
        try
        {
            var command = new CreateReservationCommand(request.ShowtimeId,
                request.SelectedSeats.Select(x => new SeatEntity(x.AuditoriumId, x.Row, x.SeatNumber)).ToList());
            await _mediator.Send(command);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Ok();
    }
}