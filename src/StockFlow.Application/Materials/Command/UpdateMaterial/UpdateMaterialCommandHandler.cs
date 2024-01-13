namespace StockFlow.Application.Materials.Command.UpdateMaterial;

public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, IResult<Material>>
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMaterialCommandHandler(IActionLogRepository actionLogRepository, IMaterialRepository materialRepository, IUnitOfWork unitOfWork)
    {
        _actionLogRepository = actionLogRepository;
        _materialRepository = materialRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<Material>> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        Material? material = await _materialRepository.GetByIdAsync(request.Id, cancellationToken);

        if (material == null) return Result<Material>.Failure("Material not found");

        material.PartNumber = request.PartNumber;
        material.SizeType = request.SizeType;
        material.Description = request.Description;

        await _materialRepository.UpdateAsync(material, cancellationToken);

        var actionLog = new ActionLog
        {
            ActionType = ActionType.Update,
            ObjectJsonValue = JsonSerializer.Serialize(material)
        };
        await _actionLogRepository.AddAsync(actionLog, cancellationToken);

        await _unitOfWork.CommitChangesAsync(cancellationToken);
        return Result<Material>.Success(material);
    }
}