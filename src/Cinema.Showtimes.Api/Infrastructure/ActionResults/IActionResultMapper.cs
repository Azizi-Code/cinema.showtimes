using System.Runtime.ExceptionServices;
using Cinema.Showtimes.Api.Common.BaseExceptions;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Infrastructure.ActionResults;

public interface IActionResultMapper<TController>
{
    IActionResult Map(Exception exception);
}

public class ActionResultMapper<TController>(IActionResultProvider actionResultProvider, ILogger<TController> logger)
    : IActionResultMapper<TController>
{
    public IActionResult Map(Exception exception)
    {
        switch (exception)
        {
            case UnAvailableServiceException:
                logger.LogError(exception, exception.Message);
                return actionResultProvider.GetServiceUnavailableErrorResponse(exception.Message);
            case UnprocessableEntityException:
                logger.LogError(exception, exception.Message);
                return actionResultProvider.GetUnprocessableEntityErrorResponse(exception.Message);
            case NotFoundException:
                logger.LogError(exception, exception.Message);
                return actionResultProvider.GetNotFoundErrorResponse(exception.Message);
            case ApplicationException:
                logger.LogError(exception, exception.Message);
                return actionResultProvider.GetApplicationErrorResponse(exception.Message);
            default:
                logger.LogError(exception, exception.Message);
                ExceptionDispatchInfo.Capture(exception).Throw();
                break;
        }

        throw new InvalidOperationException();
    }
}