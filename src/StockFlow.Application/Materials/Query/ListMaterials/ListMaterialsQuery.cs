namespace StockFlow.Application.Materials.Query.ListMaterials;

public record ListMaterialsQuery() : IRequest<IResult<List<Material>>>;