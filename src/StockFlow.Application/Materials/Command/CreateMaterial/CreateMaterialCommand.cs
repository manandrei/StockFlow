namespace StockFlow.Application.Materials.Command.CreateMaterial;

public record CreateMaterialCommand(string PartNumber, SizeType SizeType, string? Description) : IRequest<IResult<Material>>;