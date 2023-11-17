namespace StockFlow.Application.Locations.Command.CreateRack;

public class CreateRackHandler : IRequestHandler<CreateRackCommand, IResult<Rack>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRackRepository _rackRepository;
    private readonly IActionLogRepository _actionLogRepository;

    public CreateRackHandler(IUnitOfWork unitOfWork, IRackRepository rackRepository, IActionLogRepository actionLogRepository)
    {
        _unitOfWork = unitOfWork;
        _rackRepository = rackRepository;
        _actionLogRepository = actionLogRepository;
    }

    public async Task<IResult<Rack>> Handle(CreateRackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var rack = new Rack { Name = request.Name };
            await _rackRepository.AddAsync(rack, cancellationToken);
            
            var actionLog = new ActionLog
            {
                ActionType = ActionType.Add, 
                ObjectJsonValue = JsonSerializer.Serialize(rack)
            };
            await _actionLogRepository.AddAsync(actionLog, cancellationToken);
            
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            
            return Result<Rack>.Success(rack);
        }
        catch (Exception e)
        {
            // TODO: Log error
            return Result<Rack>.Failure("Could not create rack");
        }
    }
}