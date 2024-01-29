namespace StockFlow.Application.Locations.Command.CreatePosition;

public record CreatePositionCommand(string Name, long RackId, int MaxAllowed, int MinAlert, HashSet<Material> ExclusiveMaterials, List<SizeType> SizeTypes) : IRequest<IResult<Position>>;