using System.Diagnostics;

namespace Cinema.Showtimes.Api.Application.Middlewares;

public class LogRequestTimeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LogRequestTimeMiddleware> _logger;

    public LogRequestTimeMiddleware(RequestDelegate next, ILogger<LogRequestTimeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContent)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(httpContent);

        stopwatch.Stop();
        _logger.LogInformation(
            $"Request |{httpContent.Request.Path + httpContent.Request.QueryString.Value}| executed in => {stopwatch.ElapsedMilliseconds} ms");
    }
}