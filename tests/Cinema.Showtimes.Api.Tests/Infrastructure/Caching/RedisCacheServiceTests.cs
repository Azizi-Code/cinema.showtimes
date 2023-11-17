using System.Text.Json;
using Cinema.Showtimes.Api.Application.Caching;
using Cinema.Showtimes.Api.Infrastructure.Caching;
using Cinema.Showtimes.Api.Infrastructure.Exceptions;
using NSubstitute;
using StackExchange.Redis;
using Xunit;
using static Cinema.Showtimes.Api.Tests.Infrastructure.Caching.RedisCacheServiceTestHarness;

namespace Cinema.Showtimes.Api.Tests.Infrastructure.Caching;

public class RedisCacheService_GetAsync
{
    [Fact]
    public async Task DataExistWithValidKey_ReturnsData()
    {
        var database = Substitute.For<IDatabase>();
        database.StringGetAsync(DefaultCacheKey).Returns(JsonSerializer.Serialize(DefaultCacheData));
        var cacheService = CreateSut(database);

        var result = await cacheService.GetAsync<PersonTestClass>(DefaultCacheKey);

        Assert.Equal(DefaultCacheData.Name, result?.Name);
        Assert.Equal(DefaultCacheData.Family, result?.Family);
    }

    [Fact]
    public async Task DataNotExistWithValidKey_ReturnsNull()
    {
        var database = Substitute.For<IDatabase>();
        database.StringGetAsync(DefaultCacheKey).Returns(JsonSerializer.Serialize(DefaultCacheData));
        var cacheService = CreateSut(database);

        var result = await cacheService.GetAsync<PersonTestClass>("invalidKey");

        Assert.Null(result);
    }

    [Fact]
    public async Task CacheServiceIsUnAvailable_ThrowsException()
    {
        var database = Substitute.For<IDatabase>();
        database.When(x => x.StringGetAsync(DefaultCacheKey))
            .Do(x => throw new Exception(DefaultExceptionMessage));
        var redisCacheService = CreateSut(database);

        var exception = await Assert.ThrowsAsync<RedisServiceException>(() =>
            redisCacheService.GetAsync<PersonTestClass>(DefaultCacheKey));

        Assert.Equal(DefaultExceptionMessage, exception.Message);
    }
}

public class RedisCacheService_SetAsync
{
    [Fact]
    public async Task SetDataWithKey_CallsStringSetAsync()
    {
        var database = Substitute.For<IDatabase>();
        var cacheService = CreateSut(database);

        await cacheService.SetAsync(DefaultCacheKey, DefaultCacheData);

        await database.Received(1).StringSetAsync(DefaultCacheKey, JsonSerializer.Serialize(DefaultCacheData));
    }

    [Fact]
    public async Task CacheServiceIsUnAvailable_ThrowsException()
    {
        var database = Substitute.For<IDatabase>();
        database.When(x => x.StringSetAsync(DefaultCacheKey, JsonSerializer.Serialize(DefaultCacheData)))
            .Do(x => throw new Exception(DefaultExceptionMessage));
        var cacheService = CreateSut(database);

        var exception =
            await Assert.ThrowsAsync<RedisServiceException>(() =>
                cacheService.SetAsync(DefaultCacheKey, DefaultCacheData));

        Assert.Equal(DefaultExceptionMessage, exception.Message);
    }
}

public static class RedisCacheServiceTestHarness
{
    public const string DefaultExceptionMessage = "Cache service unAvailable.";
    public const string DefaultCacheKey = "cacheDataKey";
    public static readonly PersonTestClass DefaultCacheData = new() { Name = "SampleName", Family = "SampleFamily" };


    public static ICacheService CreateSut(IDatabase? database = null)
    {
        var cacheDatabase = database ?? Substitute.For<IDatabase>();
        var connection = Substitute.For<IConnectionMultiplexer>();
        connection.GetDatabase().Returns(cacheDatabase);
        return new RedisCacheService(connection);
    }

    public class PersonTestClass
    {
        public string Name { get; set; }
        public string Family { get; set; }
    }
}