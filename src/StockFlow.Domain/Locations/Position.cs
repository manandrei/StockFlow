using StockFlow.Domain.Common;
using StockFlow.Domain.Materials;
using StockFlow.Domain.Stocks;

namespace StockFlow.Domain.Locations;

public class Position : EntityBase
{
    public string Name { get; set; } = null!;
    public Rack Rack { get; set; } = null!;
    public long RackId { get; set; }
    public int MaxAllowed { get; set; }
    public int MinAlert { get; set; }
    public HashSet<Material> ExclusiveMaterials { get; set; } = new();
    public List<SizeType> SizeTypes { get; set; } = new();
    public IEnumerable<Stock> Stocks { get; set; } = Enumerable.Empty<Stock>();
}