namespace StockFlow.Application.Stocks.Command.DeleteStock;

public record DeleteStockCommand(long Id) : IRequest<IResult<Stock>>;