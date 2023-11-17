using System.Linq.Expressions;
using StockFlow.Domain.Common;

namespace StockFlow.Application.Common;

public interface IRepository<T> where T : EntityBase
{
    public Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    public Task<List<T>> GetAllAsync(bool doNotTrack = true, CancellationToken cancellationToken = default);
    public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    public Task<T?> DeleteAsync(long id, CancellationToken cancellationToken = default);
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetFilteredData(bool doNotTrack = true, Expression<Func<T, bool>>? whereQuery = null, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includes);
}