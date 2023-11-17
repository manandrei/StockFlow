namespace StockFlow.Application.Stocks.Command.CreateStock;

public class CreateStockCommandHandler : IRequestHandler<CreateStockCommand, IResult<Stock>>
{
    private readonly IStockRepository _stockRepository;
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStockCommandHandler(IStockRepository stockRepository, IActionLogRepository actionLogRepository, IUnitOfWork unitOfWork)
    {
        _stockRepository = stockRepository;
        _actionLogRepository = actionLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Stock>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
    {
        var stock = new Stock
        {
            BatchDate = request.BatchDate,
            ExpireDate = request.ExpireDate,
            MaterialId = request.MaterialId,
            PositionId = request.PositionId
        };

        await _stockRepository.AddAsync(stock, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Add,
            ObjectJsonValue = JsonSerializer.Serialize(stock)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Stock>.Success(stock);
    }
}