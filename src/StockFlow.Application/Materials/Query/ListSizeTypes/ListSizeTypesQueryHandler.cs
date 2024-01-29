namespace StockFlow.Application.Materials.Query.ListSizeTypes;

public class ListMaterialsQueryHandler : IRequestHandler<ListSizeTypesQuery, IResult<List<string>>>
{
    private readonly IMaterialRepository _materialRepository;

    public ListMaterialsQueryHandler(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<IResult<List<string>>> Handle(ListSizeTypesQuery request, CancellationToken cancellationToken)
    {
        List<string> sizeTypes = Enum.GetNames<SizeType>().ToList();
        
        return Result<List<string>>.Success(sizeTypes);
    }
}