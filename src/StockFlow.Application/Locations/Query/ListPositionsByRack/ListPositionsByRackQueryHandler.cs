namespace StockFlow.Application.Locations.Query.ListPositionsByRack;

public class ListPositionsByRackQueryHandler : IRequestHandler<ListPositionsByRackQuery, IResult<List<Position>>>
{
    private readonly IPositionRepository _positionRepository;

    public ListPositionsByRackQueryHandler(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }
    
    public async Task<IResult<List<Position>>> Handle(ListPositionsByRackQuery request, CancellationToken cancellationToken)
    {
        var positions = await _positionRepository.GetFilteredData(whereQuery: p => p.RackId == request.RackId, cancellationToken: cancellationToken);
        
        return Result<List<Position>>.Success(positions);
    }
}