namespace StockFlow.Application.Locations.Command.CreatePosition;

public record CreatePositionCommand : IRequest<Result<Position>>
{
    public required string Name { get; init; }
    public long RackId { get; init; }
    public int MaxAllowed { get; init; }
    public int MinAlert { get; init; }
    public HashSet<Material> ExclusiveMaterials { get; init; } = new();
    public List<SizeType> SizeTypes { get; init; } = new();
};