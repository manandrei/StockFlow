using StockFlow.Domain.Common;
using StockFlow.Domain.Locations;
using StockFlow.Domain.Materials;

namespace StockFlow.Domain.Stocks
{
    public class Stock : EntityBase
    {
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
        public Material Material { get; set; } = null!;
        public int MaterialId { get; set; }
        public DateOnly ExpireDate { get; set; }
        public DateOnly BatchDate { get; set; }
        public Location Location { get; set; } = null!;
        public int LocationId { get; set; }
    }
}
