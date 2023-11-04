using AddressConsultation.Infra.Cache.Implements.Interfaces;
using AddressConsultation.Infra.Cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Infra.Cache.Implements
{

    public class TokenCache<TokenDTO> : ITokenCache<TokenDTO>
    {
        public string Prefix { get; set; }

        private IPayload _payload;

        private ICacheManager _cacheManager;

        public TokenCache(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            Prefix = "Token";
            _payload = RedisCachePayload.CreateWithHours(Prefix: this.Prefix, AbsoluteExpirationInHours: 24, SlidingExpirationInHours: 6);
        }

        public async Task<TokenDTO> Get(string key) => await _cacheManager.Get<TokenDTO>(key, _payload);

        public async Task Set(string key, TokenDTO value) => await _cacheManager.Set<TokenDTO>(key, value, _payload);
    }
}
