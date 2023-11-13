using System.Net;

namespace Cinema.Showtimes.Api.Infrastructure.ActionResults;

public class ErrorResponse
{
    public Error Error { get; }

    public ErrorResponse(Error error) => Error = error;

    public static ErrorResponse Create(HttpStatusCode statusCode, string errorMessage, string parameterName) =>
        new(
            new Error(
                statusCode.ToString(),
                errorMessage,
                new[] { new ErrorDetail(parameterName) }));
}

public class Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorDetail[] Details { get; }

    public Error(string code, string message, ErrorDetail[]? details = null)
    {
        Code = code;
        Message = message;
        Details = details ?? Array.Empty<ErrorDetail>();
    }
}

public class ErrorDetail
{
    public string Target { get; }
    public ErrorMessage[] Errors { get; }

    public ErrorDetail(string target, ErrorMessage[]? errors = null)
    {
        Target = target;
        Errors = errors ?? Array.Empty<ErrorMessage>();
    }
}

public class ErrorMessage
{
    public string Message { get; }

    public ErrorMessage(string message) => Message = message;
}