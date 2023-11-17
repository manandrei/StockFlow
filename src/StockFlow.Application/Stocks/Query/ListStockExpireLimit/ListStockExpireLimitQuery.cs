namespace StockFlow.Application.Stocks.Query.ListStockExpireLimit;

public record ListStockExpireLimitQuery(int DaysBeforeExpire) : IRequest<IResult<IEnumerable<Stock>>>;