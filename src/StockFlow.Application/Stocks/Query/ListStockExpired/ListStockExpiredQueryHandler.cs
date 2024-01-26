namespace StockFlow.Application.Stocks.Query.ListStockExpired;

public class ListStockExpiredQueryHandler : IRequestHandler<ListStockExpiredQuery, IResult<IEnumerable<Stock>>>
{
    private readonly IStockRepository _stockRepository;

    public ListStockExpiredQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<IResult<IEnumerable<Stock>>> Handle(ListStockExpiredQuery request, CancellationToken cancellationToken)
    {
        List<Stock> stocks = await _stockRepository.GetFilteredData(
            whereQuery: s => s.ExpireDate < DateOnly.FromDateTime(DateTime.UtcNow),
            cancellationToken: cancellationToken,
            includes: s => new { s.Material, s.Position }
        );

        return Result<IEnumerable<Stock>>.Success(stocks);
    }
}