using AddressConsultation.Domain.Enums;

namespace AddressConsultation.Domain.Helpers
{
    public static class StateScronymConverter
    {

        public static StateScronym ToStateScronym(string stateScronym)
        {
            if (Enum.TryParse(stateScronym, out StateScronym result) && Enum.IsDefined(typeof(StateScronym), result))
            {
                return result;
            }
            throw new ArgumentException("Invalid state acronym.", nameof(stateScronym));
        }

        public static StateScronym ToStateScronym(int stateValue)
        {
            if (Enum.IsDefined(typeof(StateScronym), stateValue))
            {
                return (StateScronym)stateValue;
            }
            throw new ArgumentException("Invalid state value.", nameof(stateValue));
        }

        public static int ToInt(StateScronym stateScronym)
        {
            return (int)stateScronym;
        }

        public static string ToString(StateScronym stateScronym)
        {
            return stateScronym.ToString();
        }
    }
}
