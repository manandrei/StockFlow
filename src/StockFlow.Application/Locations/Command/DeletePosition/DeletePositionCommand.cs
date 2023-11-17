namespace StockFlow.Application.Locations.Command.DeletePosition;

public record DeletePositionCommand(long Id) : IRequest<IResult<Position>>;