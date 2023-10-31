using StockFlow.Application.Stocks;
using StockFlow.Domain.Stocks;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure.Stocks;

public class StockRepository: Repository<Stock>, IStockRepository
{
    public StockRepository(StockFlowDbContext db) : base(db)
    {
    }
}