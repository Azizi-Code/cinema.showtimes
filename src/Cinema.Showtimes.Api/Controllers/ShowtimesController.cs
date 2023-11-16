using Cinema.Showtimes.Api.Application.Commands;
using Cinema.Showtimes.Api.Application.Requests;
using Cinema.Showtimes.Api.Application.Services;
using Cinema.Showtimes.Api.Infrastructure.ActionResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Controllers;

[ApiController]
[Route("api/v1/showtimes")]
public class ShowtimesController : Controller
{
    private readonly IMoviesService _moviesService;
    private readonly IActionResultMapper<ShowtimesController> _actionResultMapper;
    private readonly IMediator _mediator;

    public ShowtimesController(IMoviesService moviesService,
        IActionResultMapper<ShowtimesController> actionResultMapper, IMediator mediator)
    {
        _moviesService = moviesService;
        _actionResultMapper = actionResultMapper;
        _mediator = mediator;
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetMovieById(string id, CancellationToken cancellationToken)
    {
        try
        {
            //to do map to our response model
            var result = await _moviesService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return _actionResultMapper.Map(exception);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateShowtimeRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateShowtimesCommand(request.AuditoriumId, request.MovieId, request.SessionDate);
            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
        catch (Exception exception)
        {
            return _actionResultMapper.Map(exception);
        }
    }
}