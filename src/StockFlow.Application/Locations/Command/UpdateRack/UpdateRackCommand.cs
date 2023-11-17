namespace StockFlow.Application.Locations.Command.UpdateRack;

public record UpdateRackCommand(long Id, string Name) : IRequest<IResult<Rack>>;