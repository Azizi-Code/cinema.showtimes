using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Application.Requests;
using Cinema.Showtimes.Api.Application.Responses;
using Cinema.Showtimes.Api.Application.Services;
using Cinema.Showtimes.Api.Infrastructure.ActionResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Controllers;

[ApiController]
[Route("api/v1/showtimes")]
public class ShowtimesController(
    IMoviesService moviesService,
    IActionResultMapper<ShowtimesController> actionResultMapper,
    IMediator mediator)
    : Controller
{
    [HttpGet("id")]
    public async Task<IActionResult> GetMovieById(string id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await moviesService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return actionResultMapper.Map(exception);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateShowtimeRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateShowtimesCommand(request.AuditoriumId, request.MovieId, request.SessionDate);
            var result = await mediator.Send<CreateShowtimeResponse>(command, cancellationToken);

            return Ok(result);
        }
        catch (Exception exception)
        {
            return actionResultMapper.Map(exception);
        }
    }
}