namespace StockFlow.Application.Materials.Command.DeleteMaterial;

public class DeleteMaterialCommandHandler : IRequestHandler<DeleteMaterialCommand, IResult<Material>>
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMaterialCommandHandler(IActionLogRepository actionLogRepository, IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
    {
        _actionLogRepository = actionLogRepository;
        _materialRepository = materialRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Material>> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(request.Id, cancellationToken);
        if (material == null) return Result<Material>.Failure("Material not found");

        await _materialRepository.DeleteAsync(material, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Remove,
            ObjectJsonValue = JsonSerializer.Serialize(material)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);

        return Result<Material>.Success(material);
    }
}
