namespace AddressConsultation.Persistence.Models
{
    public class UserModel
    {
        public UserModel(Guid userId, string username, string password, string email, string role, DateTime createdAt, DateTime lastUpdated)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
            LastUpdated = lastUpdated;
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdated { get; private set; }
    }
}
