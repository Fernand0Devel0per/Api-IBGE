using AddressConsultation.Application.DTO;

namespace AddressConsultation.Application.Infra
{
    public interface IAddressService
    {
        Task<AddressDTO> GetAddressByIbgeCodeAsync(int ibgeCode);
        Task<IEnumerable<AddressDTO>> GetAddressesByCityAsync(string cityName, int pageIndex, int pageSize);
        Task<IEnumerable<AddressDTO>> GetAddressesByStateAsync(string state, int pageIndex, int pageSize);
        Task InsertAddressAsync(AddressDTO addressDto);
        Task UpdateAddressAsync(AddressDTO addressDto);
        Task DeleteAddressAsync(string ibgeCode);
    }
}
