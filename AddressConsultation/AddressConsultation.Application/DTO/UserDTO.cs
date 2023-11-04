using PersonalException;

namespace AddressConsultation.Application.DTO;

public record UserDTO(string Username, string Email, string Password, string ConfirmPassword, string Role)
{
    public bool IsPasswordMatchingConfirmation()
    {
        return Password == ConfirmPassword;
    }
    public void ValidatePasswordConfirmation()
    {
        if (!IsPasswordMatchingConfirmation())
        {
            throw new PasswordMismatchException();
        }
    }

}


