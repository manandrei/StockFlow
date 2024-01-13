using Microsoft.EntityFrameworkCore;
using StockFlow.Application.Stocks;
using StockFlow.Domain.Stocks;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure.Stocks;

public class StockRepository : Repository<Stock>, IStockRepository
{
    private readonly StockFlowDbContext _db;

    public StockRepository(StockFlowDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Stock>> GetByPartNumberAsync(string partNumber, CancellationToken cancellationToken = default, bool doNotTrack = true)
    {
        IQueryable<Stock>? stocksQuery = doNotTrack ? _db.Stocks.AsNoTracking() : _db.Stocks;
        stocksQuery = stocksQuery.Include(s => s.Material)
            .Where(s => s.Material.PartNumber == partNumber);

        return await stocksQuery.ToListAsync(cancellationToken);
    }
}