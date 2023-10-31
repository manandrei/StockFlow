using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Common;

namespace StockFlow.Infrastructure.Common;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal readonly StockFlowDbContext Db;
    internal readonly DbSet<TEntity> DbSet;

    public Repository(StockFlowDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }
    public virtual async Task<TEntity?> GetByIdAsync(long id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return await Task.FromResult(entity);
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return await Task.FromResult(entity);
    }

    public virtual async Task<int> CountAsync()
    {
        return await DbSet.CountAsync();
    }
}