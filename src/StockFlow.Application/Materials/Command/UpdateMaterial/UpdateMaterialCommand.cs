namespace StockFlow.Application.Materials.Command.UpdateMaterial;

public record UpdateMaterialCommand(long Id, string PartNumber, SizeType SizeType, string Description) : IRequest<IResult<Material>>;