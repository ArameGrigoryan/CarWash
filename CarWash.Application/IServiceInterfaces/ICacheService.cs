namespace CarWash.Application.IServiceInterfaces;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null); // ✅ expiration-ն դարձրել optional
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}