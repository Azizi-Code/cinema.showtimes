using Cinema.Showtimes.Api.Application.Services;
using Cinema.Showtimes.Api.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Controllers;

[ApiController]
[Route("api/v1/showtimes")]
public class ShowtimesController : Controller
{
    private readonly IMoviesService _moviesService;
    private readonly IActionResultMapper<ShowtimesController> _actionResultMapper;

    public ShowtimesController(IMoviesService moviesService,
        IActionResultMapper<ShowtimesController> actionResultMapper)
    {
        _moviesService = moviesService;
        _actionResultMapper = actionResultMapper;
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetMovieById(string id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _moviesService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (Exception exception)
        {
            return _actionResultMapper.Map(exception);
        }
    }
}