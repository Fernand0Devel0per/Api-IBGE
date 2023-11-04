using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using AddressConsultation.Infra.Cache.Implements.Interfaces;
using AddressConsultation.Infra.Security;
using AddressConsultation.Infra.Security.Interface;
using AddressConsultation.Persistence.Helpers;
using AddressConsultation.Persistence.Models;
using AddressConsultation.Persistence.Repositories;
using System.Security.Authentication;

namespace AddressConsultation.Application.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository<UserModel> _userRepository;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly ITokenCache<TokenDTO> _tokenCache;

        public AuthenticationService(IUserRepository<UserModel> userRepository, IJwtAuthManager jwtAuthManager, ITokenCache<TokenDTO> tokenCache)
        {
            _userRepository = userRepository;
            _jwtAuthManager = jwtAuthManager;
            _tokenCache = tokenCache;
        }

        public async Task<string> AuthenticateAsync(LoginDTO loginDto)
        {
            UserModel user = await GetUsername(loginDto);

            if (user is null || !PasswordHelper.VerifyPassword(loginDto.Password, user.Password))
            {
                throw new AuthenticationException("Username or password is incorrect.");
            }

            return await GetToken(loginDto, user);
        }

        private async Task<string> GetToken(LoginDTO loginDto, UserModel user)
        {
            TokenDTO token = await _tokenCache.Get(loginDto.Username);

            if (token is not null) return token.Token;

            token = new TokenDTO { Token = _jwtAuthManager.GenerateToken(new UserWithRole { Role = user.Role, Username = user.Username, Email = user.Email }) };

            await _tokenCache.Set(loginDto.Username, token);

            return token.Token;
        }

        private async Task<UserModel> GetUsername(LoginDTO loginDto)
        {
            UserModel user = null;

            if (loginDto.Username.Contains("@"))
            {
                user = await _userRepository.FindByEmailAsync(loginDto.Username);

            }
            else
            {
                user = await _userRepository.FindByUsernameAsync(loginDto.Username);
            }

            return user;
        }
    }
}
