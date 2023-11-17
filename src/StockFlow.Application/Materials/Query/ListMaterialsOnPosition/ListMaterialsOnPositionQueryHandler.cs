namespace StockFlow.Application.Materials.Query.ListMaterialsOnPosition;

public class ListMaterialsOnPositionQueryHandler : IRequestHandler<ListMaterialsOnPositionQuery, IResult<List<Material>>>
{
    private readonly IMaterialRepository _materialRepository;

    public ListMaterialsOnPositionQueryHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<IResult<List<Material>>> Handle(ListMaterialsOnPositionQuery request, CancellationToken cancellationToken)
    {
        var materials = await _materialRepository.GetFilteredData(
            whereQuery: m => m.Positions.Any(p => p.Id == request.PositionId),
            includes: m => m.Stocks,
            cancellationToken: cancellationToken);

        return Result<List<Material>>.Success(materials);
    }
}