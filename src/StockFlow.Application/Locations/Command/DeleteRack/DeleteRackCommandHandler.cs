namespace StockFlow.Application.Locations.Command.DeleteRack;

public class DeleteRackCommandHandler : IRequestHandler<DeleteRackCommand, IResult<Rack>>
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IRackRepository _rackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRackCommandHandler(IActionLogRepository actionLogRepository, IRackRepository rackRepository,
        IUnitOfWork unitOfWork)
    {
        _actionLogRepository = actionLogRepository;
        _rackRepository = rackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Rack>> Handle(DeleteRackCommand request, CancellationToken cancellationToken)
    {
        var rack = await _rackRepository.GetByIdAsync(request.Id, cancellationToken);
        if (rack == null) return Result<Rack>.Failure("Rack not found");

        await _rackRepository.DeleteAsync(rack, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Remove,
            ObjectJsonValue = JsonSerializer.Serialize(rack)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Rack>.Success(rack);
    }
}