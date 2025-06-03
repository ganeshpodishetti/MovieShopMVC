using Microsoft.EntityFrameworkCore;
using MovieShop.Infra.Contracts;
using MovieShop.Infra.Data;

namespace MovieShop.Infra.Repositories;

public class BaseRepository<T>(MovieShopDbContext dbContext) : IRepository<T>
    where T : class
{
    public async Task<T> InsertAsync(T entity)
    {
        dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await dbContext.Set<T>().FindAsync(id);
        if (entity == null) return false;
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>()
                              .FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext.Set<T>()
                              .AsNoTracking()
                              .ToListAsync();
    }
}