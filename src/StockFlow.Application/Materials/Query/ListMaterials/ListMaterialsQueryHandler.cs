namespace StockFlow.Application.Materials.Query.ListMaterials;

public class ListMaterialsQueryHandler : IRequestHandler<ListMaterialsQuery, IResult<List<Material>>>
{
    private readonly IMaterialRepository _materialRepository;

    public ListMaterialsQueryHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<IResult<List<Material>>> Handle(ListMaterialsQuery request, CancellationToken cancellationToken)
    {
        var materials = await _materialRepository.GetAllAsync(cancellationToken: cancellationToken);

        return Result<List<Material>>.Success(materials);
    }
}