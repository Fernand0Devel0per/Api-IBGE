namespace AddressConsultation.Persistence.Repositories
{
    public interface IUserRepository<T> where T : class
    {
        Task<T> FindByUsernameAsync(string username);

        Task<T> FindByEmailAsync(string email);

        Task InsertAsync(T entity);

        Task UpdateAsync(T entity, Guid userId);

        Task DeleteAsync(Guid userId);
    }
}
