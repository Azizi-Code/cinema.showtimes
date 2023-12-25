using System.Net;

namespace Cinema.Showtimes.Api.Infrastructure.ActionResults;

public class ErrorResponse(Error error)
{
    public Error Error { get; } = error;

    public static ErrorResponse Create(HttpStatusCode statusCode, string errorMessage, string parameterName) =>
        new(
            new Error(
                statusCode.ToString(),
                errorMessage,
                new[] { new ErrorDetail(parameterName) }));
}

public class Error(string code, string message, ErrorDetail[]? details = null)
{
    public string Code { get; } = code;
    public string Message { get; } = message;
    public ErrorDetail[] Details { get; } = details ?? Array.Empty<ErrorDetail>();
}

public class ErrorDetail(string target, ErrorMessage[]? errors = null)
{
    public string Target { get; } = target;
    public ErrorMessage[] Errors { get; } = errors ?? Array.Empty<ErrorMessage>();
}

public class ErrorMessage(string message)
{
    public string Message { get; } = message;
}