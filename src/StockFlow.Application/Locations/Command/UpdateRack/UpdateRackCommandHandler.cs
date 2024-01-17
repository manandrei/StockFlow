namespace StockFlow.Application.Locations.Command.UpdateRack;

public class UpdateRackCommandHandler : IRequestHandler<UpdateRackCommand, IResult<Rack>>
{
    private readonly IRackRepository _rackRepository;
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRackCommandHandler(IRackRepository rackRepository, IActionLogRepository actionLogRepository, IUnitOfWork unitOfWork)
    {
        _rackRepository = rackRepository;
        _actionLogRepository = actionLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Rack>> Handle(UpdateRackCommand request, CancellationToken cancellationToken)
    {
        Rack? rack = await _rackRepository.GetByIdAsync(request.Id, cancellationToken);
        if (rack == null) return Result<Rack>.Failure("Rack not found");

        rack.Name = request.Name;

        await _rackRepository.UpdateAsync(rack, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Update,
            ObjectJsonValue = JsonSerializer.Serialize(rack)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);
        
        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Rack>.Success(rack);
    }
}