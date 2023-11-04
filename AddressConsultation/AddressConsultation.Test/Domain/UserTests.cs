using AddressConsultation.Domain.Entity;
using AddressConsultation.Domain.Enums;
using PersonalException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Test.Domain
{
    public class UserTests
    {
        [Fact]
        public void Create_ValidUser_ShouldReturnUser()
        {
            var username = "TestUser";
            var password = "Test@1234";
            var email = "test@example.com";
            var role = RoleStatus.Admin; 

            var user = User.Create(username, password, email, role);

            Assert.NotNull(user);
            Assert.Equal(username, user.Username);
            Assert.Equal(password, user.Password);
            Assert.Equal(email.ToLower(), user.Email); 
            Assert.Equal(role, user.Role);
        }

        [Fact]
        public void Create_InvalidPassword_ShouldThrowCustomValidationException()
        {
            var username = "TestUser";
            var password = "invalid";
            var email = "test@example.com";
            var role = RoleStatus.Admin;

            Assert.Throws<CustomValidationException>(() => User.Create(username, password, email, role));
        }

        [Fact]
        public void ChangeUsername_ValidUsername_ShouldChangeUsername()
        {
            var user = User.Create("TestUser", "Test@1234", "test@example.com", RoleStatus.UserDefault);
            var newUsername = "NewTestUser";

            user.ChangeUsername(newUsername);

            Assert.Equal(newUsername, user.Username);
        }

        [Fact]
        public void ChangePassword_ValidPassword_ShouldChangePassword()
        {
            var user = User.Create("TestUser", "Test@1234", "test@example.com", RoleStatus.Admin);
            var newPassword = "NewTest@1234";

            user.ChangePassword(newPassword);

            Assert.Equal(newPassword, user.Password);
        }

        [Fact]
        public void ChangeEmail_ValidEmail_ShouldChangeEmail()
        {
            var user = User.Create("TestUser", "Test@1234", "test@example.com", RoleStatus.UserDefault);
            var newEmail = "newtest@example.com";

            user.ChangeEmail(newEmail);

            Assert.Equal(newEmail.ToLower(), user.Email);
        }

        [Fact]
        public void ChangeRole_ValidRole_ShouldChangeRole()
        {
            var user = User.Create("TestUser", "Test@1234", "test@example.com", RoleStatus.UserDefault);
            var newRole = RoleStatus.Admin;  

            user.ChangeRole(newRole);

            Assert.Equal(newRole, user.Role);
        }
    }
}
