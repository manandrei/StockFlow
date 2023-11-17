namespace StockFlow.Application.Stocks;

public interface IStockRepository : IRepository<Stock>
{
    Task<IEnumerable<Stock>> GetByPartNumberAsync(string partNumber, CancellationToken cancellationToken = default, bool doNotTrack = true);
}