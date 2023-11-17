namespace StockFlow.Application.Stocks.Command.CreateStock;

public record CreateStockCommand(long MaterialId, DateOnly ExpireDate, DateOnly BatchDate, long PositionId) : IRequest<IResult<Stock>>;