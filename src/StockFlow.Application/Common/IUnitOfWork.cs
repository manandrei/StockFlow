namespace StockFlow.Application.Common;

public interface IUnitOfWork
{
    Task CommitAsync();
}