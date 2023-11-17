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
        var stocks = await _repository.GetAllAsync(cancellationToken: cancellationToken);

        return Result<IEnumerable<Stock>>.Success(stocks);
    }
}