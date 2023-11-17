namespace StockFlow.Application.Stocks.Query.ListStockByPartNumber;

public record ListStockByPartNumberQuery(string PartNumber) : IRequest<IResult<IEnumerable<Stock>>>;