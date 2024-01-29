using System.Linq.Expressions;

namespace StockFlow.Application.Stocks.Query.ListStock;

public class ListStockQueryHandler : IRequestHandler<ListStockQuery, IResult<IEnumerable<Stock>>>
{
    private readonly IStockRepository _repository;

    public ListStockQueryHandler(IStockRepository repository)
    {
        _repository = repository;
    }

    public async Task<IResult<IEnumerable<Stock>>> Handle(ListStockQuery request, CancellationToken cancellationToken)
    {
        List<Stock> stocks = await _repository.GetFilteredData(
            cancellationToken: cancellationToken,
            includes: new Expression<Func<Stock, object>>[] { s => s.Material, s => s.Position }
        );

        return Result<IEnumerable<Stock>>.Success(stocks);
    }
}