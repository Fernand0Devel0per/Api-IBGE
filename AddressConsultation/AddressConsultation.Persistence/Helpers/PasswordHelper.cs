namespace AddressConsultation.Persistence.Helpers
{
    public static class PasswordHelper
    {
        private const int workFactor = 12;
        public static string CreateHashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
            var salt = BCrypt.Net.BCrypt.GenerateSalt(workFactor);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");

            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentNullException(nameof(hashedPassword), "Hashed password cannot be null or empty.");

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
