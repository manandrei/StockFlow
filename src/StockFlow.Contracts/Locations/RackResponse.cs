namespace StockFlow.Contracts.Locations;

public record RackResponse
{
    public RackResponse(long Id, string Name, IEnumerable<PositionResponse>? Position)
    {
        this.Id = Id;
        this.Name = Name;
        this.Position = Position ?? new List<PositionResponse>();
    }

    public long Id { get; init; }
    public string Name { get; init; }

    public IEnumerable<PositionResponse> Position { get; init; }
}