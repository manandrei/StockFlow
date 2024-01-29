using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Locations.Command.CreatePosition;
using StockFlow.Application.Locations.Command.CreateRack;
using StockFlow.Application.Locations.Command.DeletePosition;
using StockFlow.Application.Locations.Command.DeleteRack;
using StockFlow.Application.Locations.Command.UpdatePosition;
using StockFlow.Application.Locations.Command.UpdateRack;
using StockFlow.Application.Locations.Query.ListPositionForMaterial;
using StockFlow.Application.Locations.Query.ListPositionsByRack;
using StockFlow.Application.Locations.Query.ListRacks;
using StockFlow.Contracts.Locations;
using StockFlow.Domain.Locations;
using StockFlow.ResultPattern;
using StockFlow.Web.Extensions;
using StockFlow.Web.Utilities;
using Swashbuckle.AspNetCore.Annotations;

namespace StockFlow.Web.API.V1;

[ApiController]
[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class LocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RackResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Get a list of all locations defined")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<RackResponse>>> ListRacks()
    {
        var query = new ListRacksQuery();
        IResult<List<Rack>> result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        IEnumerable<RackResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PositionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Get all positions for an exclusive material")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<PositionResponse>>> ListPositionsForMaterial(long materialId)
    {
        var query = new ListPositionForMaterialQuery(materialId);
        IResult<List<Position>> result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        IEnumerable<PositionResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PositionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Get all positions for an exclusive material")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<PositionResponse>>> ListPositionsForRack(long rackId)
    {
        var query = new ListPositionsByRackQuery(rackId);
        IResult<List<Position>> result = await _mediator.Send(query);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        IEnumerable<PositionResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Create a new rack")]
    [Produces("application/json")]
    public async Task<ActionResult<RackResponse>> CreateRack(AddRackRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);

        var command = new CreateRackCommand(request.Name);
        IResult<Rack> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        RackResponse response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(RackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Delete a specified rack")]
    [Produces("application/json")]
    public async Task<ActionResult<RackResponse>> DeleteRack(long rackId)
    {
        if (rackId < 1) return BadRequest(ModelState);

        var command = new DeleteRackCommand(rackId);
        IResult<Rack> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        RackResponse response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(RackResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Update the details of a specified rack")]
    [Produces("application/json")]
    public async Task<ActionResult<RackResponse>> UpdateRack(UpdateRackRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);

        var command = new UpdateRackCommand(request.Id, request.Name);
        IResult<Rack> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        RackResponse response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PositionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Create a new position in a specified rack")]
    [Produces("application/json")]
    public async Task<ActionResult<PositionResponse>> CreatePosition(AddPositionRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);

        var command = new CreatePositionCommand(request.Name, request.RackId, request.MaxAllowed, request.MinAlert, request.ExclusiveMaterials.ToDomain().ToHashSet(), request.SizeTypes.ToDomain());
        IResult<Position> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        PositionResponse response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(PositionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Delete a specified position")]
    [Produces("application/json")]
    public async Task<ActionResult<PositionResponse>> DeletePosition(long positionId)
    {
        if (positionId < 1) return BadRequest(ModelState);

        var command = new DeletePositionCommand(positionId);
        IResult<Position> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        PositionResponse response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(PositionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Update the details of a specified position")]
    [Produces("application/json")]
    public async Task<ActionResult<PositionResponse>> UpdatePosition(UpdatePositionRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);

        var command = new UpdatePositionCommand(request.Id, request.Name);
        IResult<Position> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        PositionResponse response = result.Data!.ToResponse();

        return Ok(response);
    }
}