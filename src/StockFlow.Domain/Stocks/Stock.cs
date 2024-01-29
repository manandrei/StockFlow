using StockFlow.Domain.Common;
using StockFlow.Domain.Locations;
using StockFlow.Domain.Materials;

namespace StockFlow.Domain.Stocks;

public class Stock : EntityBase
{
    public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
    public long MaterialId { get; set; }
    public Material Material { get; set; } = default!;
    public DateOnly ExpireDate { get; set; }
    public DateOnly BatchDate { get; set; }
    public long PositionId { get; set; }
    public Position Position { get; set; } = default!;
}