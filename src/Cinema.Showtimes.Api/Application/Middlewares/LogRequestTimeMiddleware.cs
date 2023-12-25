using System.Diagnostics;

namespace Cinema.Showtimes.Api.Application.Middlewares;

public class LogRequestTimeMiddleware(RequestDelegate next, ILogger<LogRequestTimeMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await next(httpContext);

        stopwatch.Stop();
        logger.LogInformation(
            $"Request |{httpContext.Request.Path + httpContext.Request.QueryString.Value}| executed in => {stopwatch.ElapsedMilliseconds} ms");
    }
}