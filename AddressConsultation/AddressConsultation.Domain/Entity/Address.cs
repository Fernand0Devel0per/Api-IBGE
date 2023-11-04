using AddressConsultation.Domain.Enums;
using AddressConsultation.Domain.Helpers;
using AddressConsultation.Domain.VO;
using PersonalDataValidations;
using PersonalException;
using System.ComponentModel.DataAnnotations;

namespace AddressConsultation.Domain.Entity
{
    public class Address : ValidatebleObject
    {
        [Required(ErrorMessage = "{0} is required.")]
        [Range(1100000, 5299999, ErrorMessage = "IBGECode must be between 1100000 and 5299999.")]
        public int IBGECode { get; private set; }

        [Required(ErrorMessage = "{0} is required.")]
        public StateScronym State { get; private set; }

        [Required(ErrorMessage = "{0} is required.")]
        public City City { get; private set; }

        private Address(int ibgeCode, StateScronym state, City city)
        {
            IBGECode = ibgeCode;
            State = state;
            City = city;
        }

        public static Address Create(int ibgeCode, StateScronym state, City city)
        {
            var addressValue = new Address(ibgeCode, state, city);
            var validationResults = addressValue.Validate();

            if (validationResults.Any()) throw new CustomValidationException(validationResults);

            return addressValue;
        }

        protected override IEnumerable<ValidationResult> CustomValidate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            int stateValue = StateScronymConverter.ToInt(State);
            int ibgeCodePrefix = IBGECode / 100000;
            if (stateValue != ibgeCodePrefix)
            {
                results.Add(new ValidationResult(
                    "The first two digits of IBGECode must match the State.",
                    new List<string> { nameof(IBGECode), nameof(State) }));
            }

            return results;
        }

        public void ChangeCity(string city)
        {
            var cityValue = City.Create(city);
            City = cityValue;
        }

        public void ChangeIbgeCode(int ibgeCode)
        {
            IBGECode = ibgeCode;
            Validate();
        }
    }
}
