using AddressConsultation.Domain.Helpers;
using PersonalDataValidations;
using PersonalException;
using System.ComponentModel.DataAnnotations;

namespace AddressConsultation.Domain.VO
{
    public class City : ValidatebleObject
    {
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(80, ErrorMessage = "Name cannot be longer than 80 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Name can only contain letters and accented characters.")]
        public string Name { get; private set; }

        private City(string name)
        {
            Name = StringFormater.FormatName(name);
        }

        public static City Create(string name)
        {
            var cityValue = new City(name);
            var validationResults = cityValue.Validate();

            if (validationResults.Any()) throw new CustomValidationException(validationResults);

            return cityValue;
        }

    }
}
