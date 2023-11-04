using AddressConsultation.Infra.Cache.Implements.Interfaces;
using AddressConsultation.Infra.Cache.Redis;

namespace AddressConsultation.Infra.Cache.Implements
{
    public class AddressCache<AddressDTO> : ICacheImplents<AddressDTO>, IAddressCache<AddressDTO>
    {
        public string Prefix { get; set; }

        private readonly IPayload _payload;

        private readonly ICacheManager _cacheManager;

        public AddressCache(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            Prefix = "Address";
            _payload = RedisCachePayload.CreateWithHours(Prefix: this.Prefix, AbsoluteExpirationInHours: 24, SlidingExpirationInHours: 1);
        }

        public async Task<AddressDTO> Get(string key) => await _cacheManager.Get<AddressDTO>(key, _payload);

        public async Task Set(string key, AddressDTO value) => await _cacheManager.Set<AddressDTO>(key, value, _payload);
    }
}
