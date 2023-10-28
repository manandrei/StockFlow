using StockFlow.Domain.Common;
using StockFlow.Domain.Stocks;
using System.Text.Json;

namespace StockFlow.Domain.ActionLogs
{
    public class ActionLog : EntityBase
    {
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.UtcNow;
        public ActionType ActionType { get; set; }
        public string ObjectJsonValue { get; set; } = null!;

        public ActionLog()
        {

        }

        public ActionLog(Stock stock, ActionType actionType)
        {
            ActionType = actionType;

            var jsonConvertOptions = new JsonSerializerOptions { WriteIndented = false };
            ObjectJsonValue = JsonSerializer.Serialize(stock, jsonConvertOptions);
        }
    }
}
