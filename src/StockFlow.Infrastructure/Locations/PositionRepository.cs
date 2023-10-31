using StockFlow.Application.Locations;
using StockFlow.Domain.Locations;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure.Locations;

public class PositionRepository: Repository<Position>, IPositionRepository
{
    public PositionRepository(StockFlowDbContext db) : base(db)
    {
    }
}