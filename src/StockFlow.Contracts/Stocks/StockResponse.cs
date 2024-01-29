using StockFlow.Contracts.Locations;
using StockFlow.Contracts.Materials;

namespace StockFlow.Contracts.Stocks;

public record StockResponse
{
    public StockResponse()
    {
    }

    public StockResponse(long id, DateTimeOffset timeStamp, MaterialResponse? material, long materialId, DateOnly expireDate, DateOnly batchDate, PositionResponse? position, long positionId)
    {
        Id = id;
        TimeStamp = timeStamp;
        Material = material;
        MaterialId = materialId;
        ExpireDate = expireDate;
        BatchDate = batchDate;
        Position = position;
        PositionId = positionId;
    }

    public long Id { get; init; }
    public DateTimeOffset TimeStamp { get; init; }
    public MaterialResponse? Material { get; init; }
    public long MaterialId { get; init; }
    public DateOnly ExpireDate { get; init; }
    public DateOnly BatchDate { get; init; }
    public PositionResponse? Position { get; init; }
    public long PositionId { get; init; }
}