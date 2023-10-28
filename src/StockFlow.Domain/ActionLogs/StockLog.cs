using StockFlow.Domain.Stocks;

namespace StockFlow.Domain.ActionLogs
{
    public class StockLog : Stock
    {
        public ActionType ActionType { get; set; }
    }
}
