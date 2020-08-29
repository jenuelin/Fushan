using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DataServices.Db
{
    public interface IRedisRepository<T> where T : class
    {
        Task<T> GetAsync(string key);
        Task AddAsync(string key, T entity);
        Task UpdateAsync(string key, T entity);
        Task DeleteAsync(string key);
    }
    public class RedisRepository<T> : IRedisRepository<T> where T : class
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions options =
            new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
        public RedisRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        async Task IRedisRepository<T>.AddAsync(string key, T entity)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(entity), options);
        }

        async Task IRedisRepository<T>.DeleteAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        async Task<T> IRedisRepository<T>.GetAsync(string key)
        {
            var result = await _cache.GetStringAsync(key);
            return string.IsNullOrEmpty(result) ? null : JsonConvert.DeserializeObject<T>(result);
        }
        async Task IRedisRepository<T>.UpdateAsync(string key, T entity)
        {
            var result = await _cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(result))
            {
                await _cache.RemoveAsync(key);
            }
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(entity), options);
        }
    }
}
