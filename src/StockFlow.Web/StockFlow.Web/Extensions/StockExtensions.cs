using StockFlow.Contracts.Stocks;
using StockFlow.Domain.Stocks;

namespace StockFlow.Web.Extensions;

public static class StockExtensions
{
    public static StockResponse ToResponse(this Stock material)
    {
        return new StockResponse
        (
            material.Id,
            material.TimeStamp,
            material.Material.ToResponse(),
            material.MaterialId,
            material.ExpireDate,
            material.BatchDate,
            material.Position.ToResponse(),
            material.PositionId
        );
    }

    public static IEnumerable<StockResponse> ToResponse(this IEnumerable<Stock> stocks)
    {
        return stocks.Select(s => s.ToResponse());
    }
}