using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Materials.Command.CreateMaterial;
using StockFlow.Application.Materials.Command.DeleteMaterial;
using StockFlow.Application.Materials.Command.UpdateMaterial;
using StockFlow.Application.Materials.Query.ListMaterials;
using StockFlow.Application.Materials.Query.ListMaterialsOnPosition;
using StockFlow.Application.Materials.Query.ListSizeTypes;
using StockFlow.Contracts.Materials;
using StockFlow.Domain.Materials;
using StockFlow.ResultPattern;
using StockFlow.Web.Extensions;
using StockFlow.Web.Utilities;
using Swashbuckle.AspNetCore.Annotations;

namespace StockFlow.Web.API.V1;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class MaterialsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaterialsController(IMediator mediator)
    {
        _mediator = mediator;
    }

  
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaterialResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Get a list of all materials defined")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<MaterialResponse>>> ListAll()
    {
        var query = new ListMaterialsQuery();
        IResult<List<Material>> result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        IEnumerable<MaterialResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MaterialResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Get a list of materials stocked on a specific position")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<MaterialResponse>>> ListByPosition(long positionId)
    {
        if (positionId < 1) return BadRequest("Position id must be greater than 0");

        var query = new ListMaterialsOnPositionQuery(positionId);
        IResult<List<Material>> result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }
        
        IEnumerable<MaterialResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Get a list of all material size types")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<string>>> SizeTypes()
    {
        var query = new ListSizeTypesQuery();
        IResult<List<string>> result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        IEnumerable<string> response = result.Data!;

        return Ok(response);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Create a new material")]
    [Produces("application/json")]
    public async Task<ActionResult<MaterialResponse>> Create(AddMaterialRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);
        
        var command = new CreateMaterialCommand(request.PartNumber, Enum.Parse<SizeType>(request.SizeType), request.Description);
        IResult<Material> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        MaterialResponse response = result.Data!.ToResponse();

        return Ok(response);
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Delete a specified material")]
    [Produces("application/json")]
    public async Task<ActionResult<MaterialResponse>> Delete(long id)
    {
        if (id < 1) return BadRequest("Id must be greater than zero");
        
        var command = new DeleteMaterialCommand(id);
        IResult<Material> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        MaterialResponse response = result.Data!.ToResponse();

        return Ok(response);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(MaterialResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Update the details of a specified material")]
    [Produces("application/json")]
    // [MapToApiVersion("2.0")]
    public async Task<ActionResult<MaterialResponse>> Update(UpdateMaterialRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);
        
        var command = new UpdateMaterialCommand(request.Id, request.PartNumber, Enum.Parse<SizeType>(request.SizeType), request.Description!);
        IResult<Material> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        MaterialResponse response = result.Data!.ToResponse();

        return Ok(response);
    }
}