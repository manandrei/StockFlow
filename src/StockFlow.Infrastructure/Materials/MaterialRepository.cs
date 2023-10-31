using StockFlow.Application.Materials;
using StockFlow.Domain.Materials;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure.Materials;

public class MaterialRepository: Repository<Material>, IMaterialRepository
{
    public MaterialRepository(StockFlowDbContext db) : base(db)
    {
    }
}