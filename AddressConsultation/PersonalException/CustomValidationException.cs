using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;

namespace PersonalException
{
    [Serializable]
    public class CustomValidationException : SerializableException
    {
        public IEnumerable<ValidationResult> ValidationResults { get; }

        public CustomValidationException(IEnumerable<ValidationResult> validationResults)
            : base(SerializeErrors(validationResults))
        {
            ValidationResults = validationResults;
        }

        private static string SerializeErrors(IEnumerable<ValidationResult> validationResults)
        {
            var errorMessages = validationResults.SelectMany(vr => vr.MemberNames.Select(mn => new { Field = mn, Error = vr.ErrorMessage }));
            return JsonSerializer.Serialize(new { message = errorMessages });
        }

        protected CustomValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            ValidationResults = (IEnumerable<ValidationResult>)info.GetValue(nameof(ValidationResults), typeof(IEnumerable<ValidationResult>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(ValidationResults), ValidationResults);

            base.GetObjectData(info, context);
        }
    }
}
