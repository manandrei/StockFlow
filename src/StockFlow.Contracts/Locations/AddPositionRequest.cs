using StockFlow.Contracts.Materials;

namespace StockFlow.Contracts.Locations;

public record AddPositionRequest
{
    public string Name { get; init; }
    public long RackId { get; init; }
    public int MaxAllowed { get; init; }
    public int MinAlert { get; init; }
    public HashSet<MaterialResponse> ExclusiveMaterials { get; init; } = new();
    public List<string> SizeTypes { get;  init;} = new();
}