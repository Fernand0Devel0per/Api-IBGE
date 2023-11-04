using AddressConsultation.Domain.Enums;

namespace AddressConsultation.Domain.Helpers
{
    public static class RoleStatusConverter
    {
        public static RoleStatus ToRoleStatus(string roleStatusString)
        {
            if (Enum.TryParse(roleStatusString, true, out RoleStatus result) && Enum.IsDefined(typeof(RoleStatus), result))
            {
                return result;
            }
            throw new ArgumentException("Invalid role status string.", nameof(roleStatusString));
        }

        public static RoleStatus ToRoleStatus(int roleStatusValue)
        {
            if (Enum.IsDefined(typeof(RoleStatus), roleStatusValue))
            {
                return (RoleStatus)roleStatusValue;
            }
            throw new ArgumentException("Invalid role status value.", nameof(roleStatusValue));
        }

        public static string ToString(RoleStatus roleStatus)
        {
            return roleStatus.ToString();
        }
    }
}
