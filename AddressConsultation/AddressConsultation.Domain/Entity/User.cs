using AddressConsultation.Domain.Enums;
using PersonalDataValidations;
using PersonalException;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Domain.Entity
{
    public class User : ValidatebleObject
    {
        [Required(ErrorMessage = "{0} is required.")]
        public Guid UserId { get; private set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(3, ErrorMessage = "{0} must be at least 3 characters long.")]
        public string Username { get; private set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MinLength(8, ErrorMessage = "{0} must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).+$", ErrorMessage = "{0} must have at least 1 lowercase letter, 1 uppercase letter, 1 symbol, and 1 number.")]
        public string Password { get; private set; }

        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress(ErrorMessage = "{0} format is invalid.")]
        public string Email { get; private set; }

        [Required(ErrorMessage = "{0} is required.")]
        public RoleStatus Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdated { get; private set; }

        private User(Guid userId , DateTime createdAt, DateTime lastUpdated, string username, string password, string email, RoleStatus role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email.ToLower();
            CreatedAt = createdAt;
            LastUpdated = lastUpdated;
            Role = role;
        }

        public static User Create(string username, string password, string email, RoleStatus role, DateTime? createdAt = null, Guid? userId = null, DateTime? lastUpdated = null)
        {
            var userValue = new User(
                userId ?? Guid.NewGuid(),
                createdAt ?? DateTime.UtcNow,
                lastUpdated ?? DateTime.UtcNow,
                username, password, email, role );
            var validationResults = userValue.Validate();

            if (validationResults.Any()) throw new CustomValidationException(validationResults);

            return userValue;
        }

        public void ChangeUsername(string newUsername)
        {
            Username = newUsername;
            LastUpdated = DateTime.UtcNow;
            Validate();
        }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
            LastUpdated = DateTime.UtcNow;
            Validate();
        }

        public void ChangeEmail(string newEmail)
        {
            Email = newEmail.ToLower();
            LastUpdated = DateTime.UtcNow;
            Validate();
        }

        public void ChangeRole(RoleStatus role)
        {
            Role = role;
            LastUpdated = DateTime.UtcNow;
            Validate();
        }
    }
}
