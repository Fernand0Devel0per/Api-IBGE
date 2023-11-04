using AddressConsultation.Infra.Cache.Implements.Interfaces;
using AddressConsultation.Infra.Cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Infra.Cache.Implements
{

    public class BlackListCache<TokenDTO> : ICacheImplents<TokenDTO>, IBlackListCache<TokenDTO>
    {
        public string Prefix { get; set; }

        private readonly IPayload _payload;

        private readonly ICacheManager _cacheManager;

        public BlackListCache(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            Prefix = "BlackList";
            _payload = RedisCachePayload.CreateWithHours(Prefix: this.Prefix, AbsoluteExpirationInHours: 24, SlidingExpirationInHours: 1);
        }

        public async Task<TokenDTO> Get(string key) => await _cacheManager.Get<TokenDTO>(key, _payload);

        public async Task Set(string key, TokenDTO value) => await _cacheManager.Set<TokenDTO>(key, value, _payload);
    }
}
