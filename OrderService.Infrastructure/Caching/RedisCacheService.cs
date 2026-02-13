using OrderService.Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetAsync(string key, object value, TimeSpan? ttl = null)
    {
        await _db.StringSetAsync(
            key,
            JsonSerializer.Serialize(value),
            ttl ?? CacheTtl.Medium
        );
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);

        if (value.IsNull)
            return default;

        // Explicit cast removes ambiguity
        return JsonSerializer.Deserialize<T>(value.ToString());
    }
}
