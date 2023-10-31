using StockFlow.Application.Common;
using StockFlow.Domain.Stocks;

namespace StockFlow.Application.Stocks;

public interface IStockRepository : IRepository<Stock>
{
}