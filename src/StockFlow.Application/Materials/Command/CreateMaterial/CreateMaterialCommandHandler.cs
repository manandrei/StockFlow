namespace StockFlow.Application.Materials.Command.CreateMaterial;

public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, IResult<Material>>
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMaterialCommandHandler(IMaterialRepository materialRepository, IActionLogRepository actionLogRepository, IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _actionLogRepository = actionLogRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Material>> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var material = new Material
            {
                PartNumber = request.PartNumber,
                SizeType = request.SizeType,
                Description = request.Description
            };
            await _materialRepository.AddAsync(material, cancellationToken);

            var actionLog = new ActionLog
            {
                ActionType = ActionType.Add,
                ObjectJsonValue = JsonSerializer.Serialize(material)
            };
            await _actionLogRepository.AddAsync(actionLog, cancellationToken);

            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return Result<Material>.Success(material);
        }
        catch (Exception)
        {
            return Result<Material>.Failure("Material creation failed");
        }
    }
}