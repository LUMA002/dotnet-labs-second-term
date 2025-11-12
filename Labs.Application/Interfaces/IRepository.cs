namespace Labs.Application.Interfaces;

/// <summary>
/// Basic interface of repository with CRUD
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get all records from the table
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();


    Task<T?> GetByIdAsync(Guid id);

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(Guid id);

    Task RemoveAsync(T entity);

    /// <summary>
    /// Save changes in DB - important for EF.
    /// </summary>
    Task<int> SaveChangesAsync();
}