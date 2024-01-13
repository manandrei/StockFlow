namespace StockFlow.Application.Locations.Command.DeletePosition;

public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, IResult<Position>>
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePositionCommandHandler(IActionLogRepository actionLogRepository, IPositionRepository positionRepository, IUnitOfWork unitOfWork)
    {
        _actionLogRepository = actionLogRepository;
        _positionRepository = positionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Position>> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        Position? position = await _positionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (position == null) return Result<Position>.Failure("Position not found");

        await _positionRepository.DeleteAsync(position, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Remove,
            ObjectJsonValue = JsonSerializer.Serialize(position)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Position>.Success(position);
    }
}