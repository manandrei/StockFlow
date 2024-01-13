using System.Linq.Expressions;

namespace StockFlow.Application.Locations.Query.ListRacks;

public class ListRacksQueryHandler : IRequestHandler<ListRacksQuery, IResult<List<Rack>>>
{
    private readonly IRackRepository _rackRepository;

    public ListRacksQueryHandler(IRackRepository rackRepository)
    {
        _rackRepository = rackRepository;
    }

    public async Task<IResult<List<Rack>>> Handle(ListRacksQuery request, CancellationToken cancellationToken)
    {
        List<Rack>? racks = await _rackRepository.GetFilteredData(includes: r => r.Positions, cancellationToken: cancellationToken);

        return Result<List<Rack>>.Success(racks);
    }
}