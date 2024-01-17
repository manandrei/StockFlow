namespace StockFlow.Contracts.Locations;

public record PositionResponse
{
    public PositionResponse(long id, string name, long rackId, int maxAllowed, int minAlert)
    {
        Id = id;
        Name = name;
        RackId = rackId;
        MaxAllowed = maxAllowed;
        MinAlert = minAlert;
    }

    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public long RackId { get; init; }
    public int MaxAllowed { get; init; }
    public int MinAlert { get; init; }
    
}