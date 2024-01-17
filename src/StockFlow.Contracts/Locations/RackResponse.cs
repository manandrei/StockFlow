namespace StockFlow.Contracts.Locations;

public record RackResponse
{
    public RackResponse(long Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    public long Id { get; init; }
    public string Name { get; init; }
}