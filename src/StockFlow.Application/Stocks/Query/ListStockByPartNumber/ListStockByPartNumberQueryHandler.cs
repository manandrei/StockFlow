namespace StockFlow.Application.Stocks.Query.ListStockByPartNumber;

public class ListStockByPartNumberQueryHandler : IRequestHandler<ListStockByPartNumberQuery, IResult<IEnumerable<Stock>>>
{
    private readonly IStockRepository _stockRepository;


    public ListStockByPartNumberQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<IResult<IEnumerable<Stock>>> Handle(ListStockByPartNumberQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Stock> stocks = await _stockRepository.GetByPartNumberAsync(request.PartNumber, cancellationToken);

        return Result<IEnumerable<Stock>>.Success(stocks);
    }
}