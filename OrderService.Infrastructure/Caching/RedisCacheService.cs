using OrderService.Application.Interfaces;
using OrderService.Application.Constants;   

using StackExchange.Redis;
using System.Text.Json;

namespace OrderService.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);

        if (value.IsNull) return default;
        // value type = RedisValue (struct from StackExchange.Redis)
        return JsonSerializer.Deserialize<T>(value.ToString());
    }

    public async Task SetAsync(string key, object value, TimeSpan? ttl)
    {
        await _db.StringSetAsync(key, JsonSerializer.Serialize(value), ttl ?? CacheTtl.Medium);
    }

}
