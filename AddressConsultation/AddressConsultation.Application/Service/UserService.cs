using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using AddressConsultation.Application.Mapping;
using AddressConsultation.Persistence.Models;
using AddressConsultation.Persistence.Repositories;
using PersonalException;

namespace AddressConsultation.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<UserModel> _userRepository; 

        public UserService(IUserRepository<UserModel> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetUserByUsernameAsync(string username)
        {
            var userModel = await _userRepository.FindByUsernameAsync(username);

            if (userModel is null)
                throw new NotFoundItemException($"Unable to find a user with the username: {username}");

            var user = userModel.MapToEntity();
            return user.MapToDTO();
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var userModel = await _userRepository.FindByEmailAsync(email);

            if (userModel is null)
                throw new NotFoundItemException($"Unable to find a user with the email: {email}");

            var user = userModel.MapToEntity();
            return user.MapToDTO();
        }

        public async Task InsertUserAsync(UserDTO userDto)
        {
            userDto.ValidatePasswordConfirmation();
            var user = userDto.MapToEntity();
            var userModel = user.MapToModel();
            await _userRepository.InsertAsync(userModel);
        }

        public async Task UpdateUserAsync(UserDTO userDto, string username)
        {
            userDto.ValidatePasswordConfirmation();
            var userId = await _userRepository.FindByUsernameAsync(username);
            var user = userDto.MapToEntity();
            var userModel = user.MapToModel();
            await _userRepository.UpdateAsync(userModel, userId.UserId);
        }

        public async Task DeleteUserAsync(string username, string email)
        {
            UserModel user = null;
            if (!string.IsNullOrEmpty(username))
                user = await _userRepository.FindByUsernameAsync(username);
            else
                user = await _userRepository.FindByEmailAsync(email);
            
            if(user is not null)
                await _userRepository.DeleteAsync(user.UserId);
        }
    }
}



