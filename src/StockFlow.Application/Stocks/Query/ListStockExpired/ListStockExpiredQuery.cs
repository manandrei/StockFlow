namespace StockFlow.Application.Stocks.Query.ListStockExpired;

public record ListStockExpiredQuery():IRequest<IResult<IEnumerable<Stock>>>;