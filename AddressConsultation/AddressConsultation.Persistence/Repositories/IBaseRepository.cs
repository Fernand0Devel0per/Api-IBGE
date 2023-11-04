namespace AddressConsultation.Persistence.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task InsertAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string entityId);
}

