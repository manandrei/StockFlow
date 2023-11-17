namespace StockFlow.Application.Locations.Command.UpdatePosition;

public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, IResult<Position>>
{
    private readonly IPositionRepository _positionRepository;
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePositionCommandHandler(IPositionRepository positionRepository, IActionLogRepository actionLogRepository, IUnitOfWork unitOfWork)
    {
        _positionRepository = positionRepository;
        _actionLogRepository = actionLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Position>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        var position = await _positionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (position == null) return Result<Position>.Failure("Position not found");

        position.Name = request.Name;
        // Todo: Add other properties like Min, Max, etc.

        await _positionRepository.UpdateAsync(position, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Update,
            ObjectJsonValue = JsonSerializer.Serialize(position)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Position>.Success(position);
    }
}