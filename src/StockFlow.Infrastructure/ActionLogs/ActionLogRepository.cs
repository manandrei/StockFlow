using StockFlow.Application.ActionLogs;
using StockFlow.Domain.ActionLogs;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure.ActionLogs;

public class ActionLogRepository: Repository<ActionLog>, IActionLogRepository
{
    public ActionLogRepository(StockFlowDbContext db) : base(db)
    {
    }
}