using AddressConsultation.Domain.Entity;
using AddressConsultation.Domain.Enums;
using AddressConsultation.Domain.VO;
using PersonalException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Test.Domain
{
    public class AddressTests
    {
        [Fact]
        public void Create_AddressWithValidData_ShouldReturnAddress()
        {
            var ibgeCode = 1100015;
            var state = StateScronym.RO;
            var city = City.Create("Some City Name");

            var address = Address.Create(ibgeCode, state, city);

            Assert.NotNull(address);
            Assert.Equal(ibgeCode, address.IBGECode);
            Assert.Equal(state, address.State);
            Assert.Equal(city, address.City);
        }

        [Fact]
        public void Create_AddressWithInvalidIBGECode_ShouldThrowCustomValidationException()
        {
            var ibgeCode = 1000000;
            var state = StateScronym.RO;
            var city = City.Create("Some City Name");

            Assert.Throws<CustomValidationException>(() => Address.Create(ibgeCode, state, city));
        }

        [Fact]
        public void ChangeCity_ValidCity_ShouldUpdateCity()
        {
            var ibgeCode = 1100015;
            var state = StateScronym.RO;
            var city = City.Create("Original City Name");

            var address = Address.Create(ibgeCode, state, city);
            var newCityName = "New City Name";

            address.ChangeCity(newCityName);

            Assert.Equal(newCityName, address.City.Name);
        }

        [Fact]
        public void ChangeIbgeCode_ValidIbgeCode_ShouldUpdateIbgeCode()
        {
            var ibgeCode = 1100015;
            var state = StateScronym.RO;
            var city = City.Create("Some City Name");

            var address = Address.Create(ibgeCode, state, city);
            var newIbgeCode = 1100020;

            address.ChangeIbgeCode(newIbgeCode);

            Assert.Equal(newIbgeCode, address.IBGECode);
        }

    }
}
