using AddressConsultation.Domain.VO;
using PersonalException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Test.Domain
{
    public class CityTests
    {
        [Fact]
        public void Create_CityWithValidName_ShouldReturnCity()
        {
            var name = "Valid City Name";

            var city = City.Create(name);

            Assert.NotNull(city);
            Assert.Equal(name, city.Name);
        }

        [Fact]
        public void Create_CityNameExceedingMaxLength_ShouldThrowCustomValidationException()
        {
            var name = new string('a', 81); // 81 caracteres.

            Assert.Throws<CustomValidationException>(() => City.Create(name));
        }

        [Fact]
        public void Create_CityNameWithNumbers_ShouldThrowCustomValidationException()
        {
            var name = "City123";

            Assert.Throws<CustomValidationException>(() => City.Create(name));
        }

        [Fact]
        public void Create_EmptyCityName_ShouldThrowCustomValidationException()
        {
            var name = "";

            Assert.Throws<CustomValidationException>(() => City.Create(name));
        }

        [Fact]
        public void Create_NullCityName_ShouldThrowCustomValidationException()
        {
            string name = null;

            Assert.Throws<CustomValidationException>(() => City.Create(name));
        }
    }
}
