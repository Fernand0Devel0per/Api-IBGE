using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PersonalDataValidations
{
    public class NotEmptyListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as ICollection;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }
    }
}
