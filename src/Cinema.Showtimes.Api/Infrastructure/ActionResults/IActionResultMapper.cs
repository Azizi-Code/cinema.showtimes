using System.Runtime.ExceptionServices;
using Cinema.Showtimes.Api.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Infrastructure.ActionResults;

public interface IActionResultMapper<TController>
{
    IActionResult Map(Exception exception);
}

public class ActionResultMapper<TController> : IActionResultMapper<TController>
{
    private readonly IActionResultProvider _actionResultProvider;
    private readonly ILogger<TController> _logger;

    public ActionResultMapper(IActionResultProvider actionResultProvider, ILogger<TController> logger)
    {
        _actionResultProvider = actionResultProvider;
        _logger = logger;
    }

    public IActionResult Map(Exception exception)
    {
        switch (exception)
        {
            case MovieApiException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetServiceUnavailableErrorResponse(exception.Message);
            case BadRequestException badRequestException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetBadRequestErrorResponse(badRequestException.Message,
                    badRequestException.Target);
            case NotFoundException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetNotFoundErrorResponse(exception.Message);
            default:
                _logger.LogError(exception, exception.Message);
                ExceptionDispatchInfo.Capture(exception).Throw();
                break;
        }

        throw new InvalidOperationException();
    }
}