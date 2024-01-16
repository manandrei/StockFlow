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
}