using System.Text.Json;
using Cinema.Showtimes.Api.Application.Caching;
using Cinema.Showtimes.Api.Infrastructure.Exceptions;
using StackExchange.Redis;

namespace Cinema.Showtimes.Api.Infrastructure.Caching;

public class RedisCacheService(IConnectionMultiplexer connectionMultiplexer) : ICacheService
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        try
        {
            var data = await _database.StringGetAsync(key);
            return !string.IsNullOrEmpty(data) ? JsonSerializer.Deserialize<T>(data) : null;
        }
        catch (Exception exception)
        {
            throw new RedisServiceException(exception.Message);
        }
    }

    public async Task SetAsync<T>(string key, T value) where T : class
    {
        try
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value));
        }
        catch (Exception exception)
        {
            throw new RedisServiceException(exception.Message);
        }
    }
}