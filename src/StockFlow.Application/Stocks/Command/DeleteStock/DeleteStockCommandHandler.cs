namespace StockFlow.Application.Stocks.Command.DeleteStock;

public class DeleteStockCommandHandler : IRequestHandler<DeleteStockCommand, IResult<Stock>>
{
    private readonly IStockRepository _stockRepository;
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteStockCommandHandler(IStockRepository stockRepository, IActionLogRepository actionLogRepository, IUnitOfWork unitOfWork)
    {
        _stockRepository = stockRepository;
        _actionLogRepository = actionLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Stock>> Handle(DeleteStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetByIdAsync(request.Id, cancellationToken);

        if (stock == null) return Result<Stock>.Failure("Stock not found");

        await _stockRepository.DeleteAsync(stock, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Remove,
            ObjectJsonValue = JsonSerializer.Serialize(stock)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Stock>.Success(stock);
    }
}