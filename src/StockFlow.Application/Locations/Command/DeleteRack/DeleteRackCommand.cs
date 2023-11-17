namespace StockFlow.Application.Locations.Command.DeleteRack;

public record DeleteRackCommand(long Id) : IRequest<IResult<Rack>>;