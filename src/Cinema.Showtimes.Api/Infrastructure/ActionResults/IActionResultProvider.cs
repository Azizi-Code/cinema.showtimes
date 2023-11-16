using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Showtimes.Api.Infrastructure.ActionResults;

public interface IActionResultProvider
{
    IActionResult GetBadRequestErrorResponse(string errorMessage, string target);
    IActionResult GetServiceUnavailableErrorResponse(string errorMessage);
    IActionResult GetUnprocessableEntityErrorResponse(string errorMessage);
    IActionResult GetNotFoundErrorResponse(string errorMessage);
    IActionResult GetApplicationErrorResponse(string errorMessage);
}

public class ActionResultProvider : IActionResultProvider
{
    public IActionResult GetBadRequestErrorResponse(string errorMessage, string target) =>
        new BadRequestObjectResult(new ErrorResponse(
            new Error(
                nameof(HttpStatusCode.BadRequest),
                errorMessage,
                new[] { new ErrorDetail(target) })
        ));

    public IActionResult GetUnprocessableEntityErrorResponse(string errorMessage) =>
        new ObjectResult(
                new ErrorResponse(
                    new Error(
                        nameof(HttpStatusCode.UnprocessableEntity),
                        errorMessage)
                )
            )
            { StatusCode = (int)HttpStatusCode.UnprocessableEntity };

    public IActionResult GetServiceUnavailableErrorResponse(string errorMessage) =>
        new ObjectResult(new ErrorResponse(
                new Error(
                    nameof(HttpStatusCode.ServiceUnavailable),
                    errorMessage)
            ))
            { StatusCode = (int)HttpStatusCode.ServiceUnavailable };

    public IActionResult GetNotFoundErrorResponse(string errorMessage) =>
        new ObjectResult(new ErrorResponse(
                new Error(
                    nameof(HttpStatusCode.NotFound),
                    errorMessage)
            ))
            { StatusCode = (int)HttpStatusCode.NotFound };

    public IActionResult GetApplicationErrorResponse(string errorMessage) =>
        new ObjectResult(new ErrorResponse(
                new Error(
                    nameof(HttpStatusCode.InternalServerError),
                    errorMessage)
            ))
            { StatusCode = (int)HttpStatusCode.InternalServerError };
}