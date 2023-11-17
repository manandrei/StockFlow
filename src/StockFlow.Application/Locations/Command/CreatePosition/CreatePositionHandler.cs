namespace StockFlow.Application.Locations.Command.CreatePosition;

public class CreatePositionHandler: IRequestHandler<CreatePositionCommand, IResult<Position>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPositionRepository _positionRepository;
    private readonly IActionLogRepository _actionLogRepository;

    public CreatePositionHandler(IUnitOfWork unitOfWork, IPositionRepository positionRepository,
        IActionLogRepository actionLogRepository)
    {
        _unitOfWork = unitOfWork;
        _positionRepository = positionRepository;
        _actionLogRepository = actionLogRepository;
    }

    public async Task<IResult<Position>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var position = new Position
            {
                Name = request.Name,
                MaxAllowed = request.MaxAllowed,
                MinAlert = request.MinAlert,
                SizeTypes = request.SizeTypes,
                RackId = request.RackId,
                ExclusiveMaterials = request.ExclusiveMaterials
            };
            await _positionRepository.AddAsync(position, cancellationToken);

            var actionLog = new ActionLog
            {
                ActionType = ActionType.Add,
                ObjectJsonValue = JsonSerializer.Serialize(position)
            };
            await _actionLogRepository.AddAsync(actionLog, cancellationToken);

            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return Result<Position>.Success(position);
        }
        catch (Exception e)
        {
            return Result<Position>.Failure("Could not create the position");
        }
    }
}