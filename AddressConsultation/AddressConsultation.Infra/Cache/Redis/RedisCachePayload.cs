using Microsoft.Extensions.Caching.Distributed;
using PersonalException;
using System;

namespace AddressConsultation.Infra.Cache.Redis
{
    public class RedisCachePayload : IPayload
    {
        public string Prefix { get; private set; }
        public DistributedCacheEntryOptions Options { get; private set; }

        private RedisCachePayload(string prefix, DistributedCacheEntryOptions options)
        {
            Prefix = prefix;
            Options = options;
        }

        public static RedisCachePayload Create(string Prefix,
                                       int AbsoluteExpirationInSeconds,
                                       int SlidingExpirationInSeconds)
        {
            ValidateExpirationValues(AbsoluteExpirationInSeconds, SlidingExpirationInSeconds);
            return new RedisCachePayload(Prefix ?? string.Empty,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(AbsoluteExpirationInSeconds),
                    SlidingExpiration = TimeSpan.FromSeconds(SlidingExpirationInSeconds)
                });
        }

        public static RedisCachePayload CreateWithMinutes(string Prefix,
                                                  int AbsoluteExpirationInMinutes,
                                                  int SlidingExpirationInMinutes)
        {
            ValidateExpirationValues(AbsoluteExpirationInMinutes, SlidingExpirationInMinutes);
            return new RedisCachePayload(Prefix ?? string.Empty,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteExpirationInMinutes),
                    SlidingExpiration = TimeSpan.FromMinutes(SlidingExpirationInMinutes)
                }); 
        }
        public static RedisCachePayload CreateWithHours(string Prefix,
                                           int AbsoluteExpirationInHours,
                                           int SlidingExpirationInHours)
        {
            ValidateExpirationValues(AbsoluteExpirationInHours, SlidingExpirationInHours);
            return new RedisCachePayload(Prefix ?? string.Empty,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(AbsoluteExpirationInHours),
                    SlidingExpiration =  TimeSpan.FromHours(SlidingExpirationInHours)
                }); 
        }

        private static void ValidateExpirationValues(int absoluteExpiration, int slidingExpiration)
        {
            if (absoluteExpiration < 0 || slidingExpiration < 0)
            {
                throw new InvalidCacheConfigurationException("Expiration values must be non-negative.");
            }
        }
    }
}
