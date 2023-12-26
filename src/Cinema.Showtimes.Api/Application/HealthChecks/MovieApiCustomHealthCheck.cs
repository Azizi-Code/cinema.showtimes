using Cinema.Showtimes.Api.Application.Clients;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cinema.Showtimes.Api.Application.HealthChecks;

public class MovieApiCustomHealthCheck(IMoviesApiClient movieApiClient) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            await movieApiClient.GetByIdAsync(string.Empty, cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy(exception.Message);
        }
    }
}