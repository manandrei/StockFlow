namespace StockFlow.Application.Locations.Query.ListPositionForMaterial;

public record ListPositionForMaterialQuery(long MaterialId) : IRequest<List<Position>>;