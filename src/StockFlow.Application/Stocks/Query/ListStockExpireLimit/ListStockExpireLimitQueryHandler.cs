namespace StockFlow.Application.Stocks.Query.ListStockExpireLimit;

public class ListStockExpireLimitQueryHandler : IRequestHandler<ListStockExpireLimitQuery, IResult<IEnumerable<Stock>>>
{
    private readonly IStockRepository _stockRepository;

    public ListStockExpireLimitQueryHandler(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    public async Task<IResult<IEnumerable<Stock>>> Handle(ListStockExpireLimitQuery request, CancellationToken cancellationToken)
    {
        int daysBeforeExpire;
        if (request.DaysBeforeExpire > 0)
        {
            daysBeforeExpire = request.DaysBeforeExpire * -1;
        }
        else
        {
            daysBeforeExpire = request.DaysBeforeExpire;
        }

        var expDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(daysBeforeExpire));

        var stocks = await _stockRepository.GetFilteredData(
            whereQuery: s => s.ExpireDate < expDate,
            cancellationToken: cancellationToken,
            includes: s => new { s.Material, s.Position }
        );

        return Result<IEnumerable<Stock>>.Success(stocks);
    }
}