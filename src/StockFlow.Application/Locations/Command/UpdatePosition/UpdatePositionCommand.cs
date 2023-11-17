namespace StockFlow.Application.Locations.Command.UpdatePosition;

//Todo: Add other properties like Min, Max, etc.
public record UpdatePositionCommand(long Id, string Name) : IRequest<IResult<Position>>;