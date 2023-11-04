using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AddressConsultation.Infra.Cache.Redis
{
    public class RedisCacheService : ICacheManager 
    {
    
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache )
        {
            _cache = cache;
        }

        public async Task Set<T>(string key, T value, IPayload payload)
        {
            RedisCachePayload redisCachePayload = payload as RedisCachePayload;
            string fullKey = $"{redisCachePayload.Prefix}_{key}";

            var serializedValue = JsonConvert.SerializeObject(value);

            await _cache.SetStringAsync(fullKey, serializedValue, redisCachePayload.Options);
        }

        public async Task<T> Get<T>(string key, IPayload payload)
        {
            RedisCachePayload redisCachePayload = payload as RedisCachePayload;
            string fullKey = $"{redisCachePayload.Prefix}_{key}";

            var serializedValue = await _cache.GetStringAsync(fullKey);

            if (string.IsNullOrEmpty(serializedValue))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(serializedValue);
        }
    }
}
