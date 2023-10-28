using StockFlow.Domain.Common;

namespace StockFlow.Domain.Locations
{
    public class Rack : EntityBase
    {
        public string Name { get; set; } = null!;
        public IEnumerable<Location> Locations { get; set; } = Enumerable.Empty<Location>();
    }
}
