using StockFlow.Contracts.Materials;
using StockFlow.Domain.Materials;

namespace StockFlow.Web.Extensions;

public static class MaterialExtensions
{
    public static MaterialResponse ToResponse(this Material material)
    {
        return new MaterialResponse
        (
            material.Id,
            material.PartNumber,
            material.SizeType.ToString(),
            material.Description
        );
    }

    public static IEnumerable<MaterialResponse> ToResponse(this IEnumerable<Material> materials)
    {
        return materials.Select(m => m.ToResponse());
    }

    public static Material ToDomain(this MaterialResponse material)
    {
        return new Material
        {
            Id = material.Id,
            PartNumber = material.PartNumber,
            SizeType = Enum.Parse<SizeType>(material.SizeType),
            Description = material.Description
        };
    }

    public static IEnumerable<Material> ToDomain(this IEnumerable<MaterialResponse> materials)
    {
        return materials.Select(m => m.ToDomain());
    }

    public static List<SizeType> ToDomain(this IEnumerable<string> sizeTypes)
    {
        return sizeTypes.Select(Enum.Parse<SizeType>).ToList();
    }
}