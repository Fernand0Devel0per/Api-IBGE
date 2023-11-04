using AddressConsultation.Application.DTO;

namespace AddressConsultation.Application.Infra
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(LoginDTO loginDTO);
    }
}
