using PersonalException;
using System.ComponentModel.DataAnnotations;

namespace PersonalDataValidations;

public abstract class ValidatebleObject
{
    protected virtual IEnumerable<ValidationResult> CustomValidate(ValidationContext validationContext)
    {
        return Enumerable.Empty<ValidationResult>();
    }

    protected ICollection<ValidationResult> Validate()
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(this);
        Validator.TryValidateObject(this, validationContext, validationResults, true);

        validationResults.AddRange(CustomValidate(validationContext));

        return validationResults;
    }

    protected void ValidateAndThrowIfInvalid(object value, ValidationContext validationContext)
    {
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateProperty(value, validationContext, validationResults))
        {
            throw new CustomValidationException(validationResults);
        }
    }
}

