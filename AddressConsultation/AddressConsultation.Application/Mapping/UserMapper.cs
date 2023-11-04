using AddressConsultation.Application.DTO;
using AddressConsultation.Domain.Entity;
using AddressConsultation.Domain.Helpers;
using AddressConsultation.Persistence.Helpers;
using AddressConsultation.Persistence.Models;

namespace AddressConsultation.Application.Mapping
{
    public static class UserMapper
    {
        public static User MapToEntity(this UserDTO userDTO)
        {
            var role = RoleStatusConverter.ToRoleStatus(userDTO.Role);
            var user = User.Create(userDTO.Username, userDTO.Password, userDTO.Email, role);
            return user;
        }

        public static User MapToEntity(this UserModel userModel)
        {
            var role = RoleStatusConverter.ToRoleStatus(userModel.Role);
            var user = User.Create(userModel.Username, userModel.Password, userModel.Email, role, userModel.CreatedAt, userModel.UserId, userModel.LastUpdated);
            return user;
        }

        public static UserDTO MapToDTO(this User user)
        {
            var role = RoleStatusConverter.ToString(user.Role);
            return new (user.Username, user.Email, user.Password, string.Empty, role);
        }

        public static UserModel MapToModel(this User user)
        {
            var role = RoleStatusConverter.ToString(user.Role);
            var passowordHash = PasswordHelper.CreateHashPassword(user.Password);
            return new (user.UserId, user.Username, passowordHash, user.Email, role, user.CreatedAt, user.LastUpdated);
        }
    }
}
