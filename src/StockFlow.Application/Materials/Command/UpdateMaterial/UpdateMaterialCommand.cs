namespace StockFlow.Application.Materials.Command.UpdateMaterial;

public record UpdateMaterialCommand(long Id, string PartNumber) : IRequest<IResult<Material>>;