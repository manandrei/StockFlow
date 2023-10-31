using StockFlow.Application.Locations;
using StockFlow.Domain.Locations;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure.Locations;

public class RackRepository: Repository<Rack>, IRackRepository
{
    public RackRepository(StockFlowDbContext db) : base(db)
    {
    }
}