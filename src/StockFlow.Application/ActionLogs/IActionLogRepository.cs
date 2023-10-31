using StockFlow.Application.Common;
using StockFlow.Domain.ActionLogs;

namespace StockFlow.Application.ActionLogs;

public interface IActionLogRepository : IRepository<ActionLog>
{
    
}