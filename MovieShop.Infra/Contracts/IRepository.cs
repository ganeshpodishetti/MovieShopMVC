namespace MovieShop.Infra.Contracts;

public interface IRepository<T> where T : class
{
    Task<T> InsertAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}