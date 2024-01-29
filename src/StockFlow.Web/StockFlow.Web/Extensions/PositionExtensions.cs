using StockFlow.Contracts.Locations;
using StockFlow.Domain.Locations;

namespace StockFlow.Web.Extensions;

public static class PositionExtensions
{
    public static PositionResponse ToResponse(this Position position)
    {
        return new PositionResponse
        (
            position.Id,
            position.Name,
            position.RackId,
            position.MaxAllowed,
            position.MinAlert
        );
    }
    
    public static IEnumerable<PositionResponse> ToResponse(this IEnumerable<Position> positions)
    {
        return positions.Select(p => p.ToResponse());
    }
}