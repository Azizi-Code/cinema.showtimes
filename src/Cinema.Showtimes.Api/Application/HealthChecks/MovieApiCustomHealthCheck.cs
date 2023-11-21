using Cinema.Showtimes.Api.Application.Clients;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cinema.Showtimes.Api.Application.HealthChecks;

public class MovieApiCustomHealthCheck : IHealthCheck
{
    private readonly IMoviesApiClient _movieApiClient;

    public MovieApiCustomHealthCheck(IMoviesApiClient movieApiClient)
    {
        _movieApiClient = movieApiClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            await _movieApiClient.GetByIdAsync(string.Empty, cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy(exception.Message);
        }
    }
}