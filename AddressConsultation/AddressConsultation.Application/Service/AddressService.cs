using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using AddressConsultation.Application.Mapping;
using AddressConsultation.Domain.Helpers;
using AddressConsultation.Infra.Cache.Implements.Interfaces;
using AddressConsultation.Persistence.Models;
using AddressConsultation.Persistence.Repositories;
using PersonalException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Application.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository<AddressModel> _addressRepository;
        private readonly IAddressCache<AddressDTO> _addressCache;

        public AddressService(IAddressRepository<AddressModel> addressRepository, IAddressCache<AddressDTO> addressCache)
        {
            _addressRepository = addressRepository;
            _addressCache = addressCache;
        }

        public async Task<AddressDTO> GetAddressByIbgeCodeAsync(int ibgeCode)
        {
            var addressDto = await _addressCache.Get(ibgeCode.ToString());

            if (addressDto is not null) return addressDto;
            var addressModel = await _addressRepository.FindByIBGECodeAsync(ibgeCode.ToString());

            if (addressModel is null)
                throw new NotFoundItemException($"Unable to find a record for the given IBGE code: {ibgeCode}");

            var address = addressModel.MapToEntity();

            addressDto = address.MapToDTO();
            await _addressCache.Set(address.IBGECode.ToString(), addressDto);
            return addressDto;
        }

        public async Task<IEnumerable<AddressDTO>> GetAddressesByCityAsync(string cityName, int pageIndex = 1, int pageSize = 10)
        {
            ValidatePagination(pageIndex, pageSize);
            var addressModels = await _addressRepository.FindByCityAsync(StringFormater.FormatName(cityName), pageIndex, pageSize);
            var addresses = addressModels.Select(model => model.MapToEntity());
            return addresses.Select(entity => entity.MapToDTO());
        }

        public async Task<IEnumerable<AddressDTO>> GetAddressesByStateAsync(string state, int pageIndex = 1, int pageSize = 10)
        {
            ValidatePagination(pageIndex, pageSize);
            var addressModels = await _addressRepository.FindByStateAsync(state, pageIndex, pageSize);
            var addresses = addressModels.Select(model => model.MapToEntity());
            return addresses.Select(entity => entity.MapToDTO());
        }

        public async Task InsertAddressAsync(AddressDTO addressDto)
        {
            var address = addressDto.MapToEntity();
            var addressModel = address.MapToModel();
            await _addressRepository.InsertAsync(addressModel);
        }

        public async Task UpdateAddressAsync(AddressDTO addressDto)
        {
            var address = addressDto.MapToEntity();
            var addressModel = address.MapToModel();
            await _addressRepository.UpdateAsync(addressModel);
        }

        public async Task DeleteAddressAsync(string ibgeCode)
        {
            await _addressRepository.DeleteAsync(ibgeCode);
        }

        private void ValidatePagination(int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                throw new ArgumentOutOfRangeException(nameof(pageIndex), "PageIndex must be 1 or greater.");

            if (pageSize < 1 || pageSize > 100)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be between 1 and 100.");
        }
    }
}
