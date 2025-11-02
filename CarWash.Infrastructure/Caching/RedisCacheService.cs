using CarWash.Application.IServiceInterfaces;
using Newtonsoft.Json;

namespace CarWash.Infrastructure.Caching;
using StackExchange.Redis;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _db = redis.GetDatabase();
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        string json = JsonConvert.SerializeObject(value);
        await _db.StringSetAsync(key, json, expiration);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty) return default;
        return JsonConvert.DeserializeObject<T>(value);
    }

    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(key);
    }
}

