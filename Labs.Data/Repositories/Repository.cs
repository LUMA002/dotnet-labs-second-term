using Labs.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labs.Data.Repositories;

/// <summary>
/// Universal CRUD repository for all entities.
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ReservationContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(ReservationContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        // SELECT * FROM [Table]
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        // SELECT * FROM [Table] WHERE Id = @id
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        // INSERT INTO [Table] VALUES (..)
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        // UPDATE [Table] SET .. WHERE Id = ..
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            // DELETE FROM [Table] WHERE Id = @id
            _dbSet.Remove(entity);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        // do all changes in DB and save them
        return await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity); //sychronous operation in EF (just marking to deletion)

        await Task.CompletedTask; // plug to keep async signature
    }
}