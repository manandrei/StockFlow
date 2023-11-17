namespace StockFlow.Application.Materials.Query.ListMaterialsOnPosition;

public record ListMaterialsOnPositionQuery(long PositionId) : IRequest<IResult<List<Material>>>;