using System.Runtime.ExceptionServices;
using Cinema.Showtimes.Api.Common.BaseExceptions;
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
            case UnAvailableServiceException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetServiceUnavailableErrorResponse(exception.Message);
            case UnprocessableEntityException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetUnprocessableEntityErrorResponse(exception.Message);
            case NotFoundException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetNotFoundErrorResponse(exception.Message);
            case ApplicationException:
                _logger.LogError(exception, exception.Message);
                return _actionResultProvider.GetApplicationErrorResponse(exception.Message);
            default:
                _logger.LogError(exception, exception.Message);
                ExceptionDispatchInfo.Capture(exception).Throw();
                break;
        }

        throw new InvalidOperationException();
    }
}