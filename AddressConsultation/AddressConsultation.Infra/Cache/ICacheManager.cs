namespace AddressConsultation.Infra.Cache
{
    public interface ICacheManager
    {
        Task<T> Get<T>(string key, IPayload payload);
        Task Set<T>(string key, T value, IPayload payload);
    }
}
