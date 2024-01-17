using StockFlow.Contracts.Locations;
using StockFlow.Domain.Locations;

namespace StockFlow.Web.Extensions;

public static class RackExtensions
{
    public static RackResponse ToResponse(this Rack rack)
    {
        return new RackResponse
        (
            rack.Id,
            rack.Name
        );
    }
    
    public static IEnumerable<RackResponse> ToResponse(this IEnumerable<Rack> racks)
    {
        return racks.Select(r => r.ToResponse());
    }
}