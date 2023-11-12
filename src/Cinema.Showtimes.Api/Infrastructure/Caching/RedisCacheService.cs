using System.Text.Json;
using Cinema.Showtimes.Api.Application.Caching;
using StackExchange.Redis;

namespace Cinema.Showtimes.Api.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer) =>
        _database = connectionMultiplexer.GetDatabase();

    public async Task<T?> GetAsync<T>(string key) where T : class
    {
        var data = await _database.StringGetAsync(key);

        return !string.IsNullOrEmpty(data) ? JsonSerializer.Deserialize<T>(data) : null;
    }

    public async Task SetAsync<T>(string key, T value) where T : class =>
        await _database.StringSetAsync(key, JsonSerializer.Serialize(value));
}