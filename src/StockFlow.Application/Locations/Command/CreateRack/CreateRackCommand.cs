namespace StockFlow.Application.Locations.Command.CreateRack;

public record CreateRackCommand(string Name) : IRequest<IResult<Rack>>;