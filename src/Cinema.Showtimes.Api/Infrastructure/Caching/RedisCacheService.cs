using System.Text.Json;
using Cinema.Showtimes.Api.Application.Caching;
using Cinema.Showtimes.Api.Infrastructure.Exceptions;
using StackExchange.Redis;

namespace Cinema.Showtimes.Api.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer) =>
        _database = connectionMultiplexer.GetDatabase();

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        try
        {
            var data = await _database.StringGetAsync(key);

            return !string.IsNullOrEmpty(data) ? JsonSerializer.Deserialize<T>(data) : null;
        }
        catch
        {
            throw new RedisServiceException();
        }
    }

    public async Task SetAsync<T>(string key, T value) where T : class
    {
        try
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value));
        }
        catch
        {
            throw new RedisServiceException();
        }
    }
}