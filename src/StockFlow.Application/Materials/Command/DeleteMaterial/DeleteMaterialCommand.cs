namespace StockFlow.Application.Materials.Command.DeleteMaterial;

public record DeleteMaterialCommand(long Id) : IRequest<IResult<Material>>;
