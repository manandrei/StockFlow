namespace StockFlow.Application.Locations.Query.ListRacks;

public record ListRacksQuery() : IRequest<IResult<List<Rack>>>;