namespace StockFlow.Application.Common;

public interface IRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(long id);
    public Task<List<T>> GetAllAsync();
    public Task<T> AddAsync(T entity);
    public Task<T> UpdateAsync(T entity);
    public Task<T> DeleteAsync(T entity);
    public Task<int> CountAsync();
}