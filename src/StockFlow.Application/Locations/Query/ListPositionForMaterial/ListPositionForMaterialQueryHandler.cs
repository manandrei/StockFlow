namespace StockFlow.Application.Locations.Query.ListPositionForMaterial;

public class ListPositionForMaterialQueryHandler : IRequestHandler<ListPositionForMaterialQuery, List<Position>>
{
    private readonly IPositionRepository _positionRepository;

    public ListPositionForMaterialQueryHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<List<Position>> Handle(ListPositionForMaterialQuery request, CancellationToken cancellationToken)
    {
        var positions = await _positionRepository.GetFilteredData(
            whereQuery: p => p.ExclusiveMaterials
                .Any(mat => mat.Id == request.MaterialId),
            cancellationToken: cancellationToken);

        return positions;
    }
}