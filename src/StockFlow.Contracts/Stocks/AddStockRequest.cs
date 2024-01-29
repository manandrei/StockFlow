namespace StockFlow.Contracts.Stocks;

public record AddStockRequest
{
    public long MaterialId { get; init; }
    public DateOnly ExpireDate { get; init; }
    public DateOnly BatchDate { get; init; }
    public long PositionId { get; init; }
}