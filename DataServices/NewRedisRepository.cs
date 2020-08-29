using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataServices.Db
{
    public interface INewRedisRepository
    {
        Task<RedisValue> GetAsync<T>(string key);
        Task AddAsync<T>(string key, T entity);
        Task UpdateAsync<T>(string prefix, string key, T entity);
        Task DeleteAsync(string key);
        Task<RedisValue> GetHashAsync<T>(string prefix, string key);
        Task<T[]> GetHashAllAsync<T>(string prefix);
        Task AddHashAsync<T>(string prefix, string key, T entity);
        Task AddHashAsync<T>(string prefix, HashEntry[] entities);

        Task DeleteHashAsync(string prefix, string key);

        Task ListRightPushAsync(string prefix, string key);
    }
    public class NewRedisRepository : INewRedisRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabase database;
        private static readonly TimeSpan ttl = TimeSpan.FromSeconds(30);
        public NewRedisRepository(IConfiguration configuration)
        {
            _configuration = configuration;

            var config = _configuration.GetValue<string>("Redis:Connection");

            database = ConnectionMultiplexer.Connect(config).GetDatabase(0);
        }

        async Task INewRedisRepository.AddAsync<T>(string key, T entity)
        {
            await database.StringSetAsync(key, JsonConvert.SerializeObject(entity), ttl);
        }

        async Task INewRedisRepository.AddHashAsync<T>(string prefix, string key, T entity)
        {
            await database.HashSetAsync(prefix, key, JsonConvert.SerializeObject(entity));
        }

        async Task INewRedisRepository.DeleteAsync(string key)
        {
            await database.KeyDeleteAsync(key);
        }

        Task<RedisValue> INewRedisRepository.GetAsync<T>(string key)
        {
            return database.StringGetAsync(key);
        }

        Task<RedisValue> INewRedisRepository.GetHashAsync<T>(string prefix, string key)
        {
            return database.HashGetAsync(prefix, key);
        }
        async Task<T[]> INewRedisRepository.GetHashAllAsync<T>(string prefix)
        {
            var result = await database.HashValuesAsync(prefix);
            return result.Select(r => JsonConvert.DeserializeObject<T>(r)).ToArray();
        }

        async Task INewRedisRepository.UpdateAsync<T>(string prefix, string key, T entity)
        {
            var result = await database.StringGetAsync(key);
            if (!string.IsNullOrEmpty(result))
            {
                await database.KeyDeleteAsync(key);
            }
            await database.HashSetAsync(prefix, key, JsonConvert.SerializeObject(entity));
        }

        async Task INewRedisRepository.AddHashAsync<T>(string prefix, HashEntry[] entities)
        {
            await database.HashSetAsync(prefix, entities);
        }

        Task INewRedisRepository.DeleteHashAsync(string prefix, string key)
        {
            return database.HashDeleteAsync(prefix, key);
        }

        Task INewRedisRepository.ListRightPushAsync(string prefix, string key)
        {
            return database.ListRightPushAsync(prefix, key);
        }
    }
}
