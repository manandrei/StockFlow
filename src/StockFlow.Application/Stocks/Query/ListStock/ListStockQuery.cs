namespace StockFlow.Application.Stocks.Query.ListStock;

public record ListStockQuery() : IRequest<IResult<IEnumerable<Stock>>>;