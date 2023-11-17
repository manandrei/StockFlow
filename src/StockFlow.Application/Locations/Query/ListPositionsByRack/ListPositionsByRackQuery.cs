namespace StockFlow.Application.Locations.Query.ListPositionsByRack;

public record ListPositionsByRackQuery(long RackId) : IRequest<IResult<List<Position>>>;