namespace StockFlow.Application.Locations.Query.ListPositionForMaterial;

public class ListPositionForMaterialQueryHandler : IRequestHandler<ListPositionForMaterialQuery, IResult<List<Position>>>
{
    private readonly IPositionRepository _positionRepository;

    public ListPositionForMaterialQueryHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IResult<List<Position>>> Handle(ListPositionForMaterialQuery request, CancellationToken cancellationToken)
    {
        // Todo: Check if material exists
        List<Position> positions = await _positionRepository.GetFilteredData(
            whereQuery: p => p.ExclusiveMaterials
                .Any(mat => mat.Id == request.MaterialId),
            cancellationToken: cancellationToken);

        return Result<List<Position>>.Success(positions);
    }
}