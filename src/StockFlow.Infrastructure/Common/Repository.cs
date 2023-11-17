using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Common;
using System.Linq.Expressions;
using StockFlow.Domain.Common;

namespace StockFlow.Infrastructure.Common;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly StockFlowDbContext _db;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(StockFlowDbContext db)
    {
        _db = db;
        _dbSet = db.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetAllAsync(bool doNotTrack = true, CancellationToken cancellationToken = default)
    {
        var query = doNotTrack ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetFilteredData(bool doNotTrack = true, Expression<Func<TEntity, bool>>? whereQuery = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[]? includes)
    {
        var query = doNotTrack ? _dbSet.AsNoTracking() : _dbSet.AsQueryable();

        if (whereQuery != null)
            query = query
                .Where(whereQuery);

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        return await Task.FromResult(entity);
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        return await Task.FromResult(entity);
    }

    public async Task<TEntity?> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
       var entity = await _dbSet.Where(w => w.Id == id)
           .FirstOrDefaultAsync(cancellationToken);
       
       if (entity is null) return null;
       
       return await DeleteAsync(entity, cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }
}