using AddressConsultation.Application.DTO;
using AddressConsultation.Domain.Entity;
using AddressConsultation.Domain.Helpers;
using AddressConsultation.Domain.VO;
using AddressConsultation.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Application.Mapping
{
    public static class AddressMapper
    {
        public static Address MapToEntity(this AddressDTO addressDto)
        {
            var city = City.Create(addressDto.City);
            var state = StateScronymConverter.ToStateScronym(addressDto.State);
            var address = Address.Create(addressDto.IBGECode,state,city);
            return address;
        }

        public static Address MapToEntity(this AddressModel addressModel)
        {
            var city = City.Create(addressModel.City);
            var state = StateScronymConverter.ToStateScronym(addressModel.State);
            var ibgeCode = Convert.ToInt32(addressModel.Id);
            var address = Address.Create(ibgeCode, state, city);
            return address;
        }

        public static AddressDTO MapToDTO(this Address address)
        {
            var state = StateScronymConverter.ToString(address.State);
            return new(address.IBGECode, state, address.City.Name);
        }

        public static AddressModel MapToModel(this Address address)
        {
            var id = address.IBGECode.ToString();
            var state = StateScronymConverter.ToString(address.State);
            return new(id, state, address.City.Name);
        }
    }
}
