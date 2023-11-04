using AddressConsultation.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressConsultation.Application.Infra
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByUsernameAsync(string username);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task InsertUserAsync(UserDTO userDto);
        Task UpdateUserAsync(UserDTO userDto, string username);
        Task DeleteUserAsync(string username, string email);
    }
}
