namespace StockFlow.Application.Materials.Query.ListSizeTypes;

public record ListSizeTypesQuery() : IRequest<IResult<List<string>>>;