using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockFlow.Application.Stocks.Command.CreateStock;
using StockFlow.Application.Stocks.Command.DeleteStock;
using StockFlow.Application.Stocks.Query.ListStock;
using StockFlow.Application.Stocks.Query.ListStockByPartNumber;
using StockFlow.Application.Stocks.Query.ListStockExpired;
using StockFlow.Application.Stocks.Query.ListStockExpireLimit;
using StockFlow.Contracts.Stocks;
using StockFlow.Domain.Stocks;
using StockFlow.ResultPattern;
using StockFlow.Web.Extensions;
using StockFlow.Web.Utilities;
using Swashbuckle.AspNetCore.Annotations;

namespace StockFlow.Web.API.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiVersion(ApiVersions.V1)]
public class StocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public StocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation("Returns a list of all materials in stock")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<StockResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListAll()
    {
        var query = new ListStockQuery();
        IResult<IEnumerable<Stock>> result = await _mediator.Send(query);

        if (result.IsFailure) return BadRequest(result.ErrorMessage);

        IEnumerable<StockResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation("Returns a list of materials in stock for a specific part number")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<StockResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListByPartNumber(string partNumber)
    {
        var query = new ListStockByPartNumberQuery(partNumber);
        IResult<IEnumerable<Stock>> result = await _mediator.Send(query);

        if (result.IsFailure) return BadRequest(result.ErrorMessage);

        IEnumerable<StockResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation("Returns a list of expired material in stock")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<StockResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListExpired()
    {
        var query = new ListStockExpiredQuery();
        IResult<IEnumerable<Stock>> result = await _mediator.Send(query);

        if (result.IsFailure) return BadRequest(result.ErrorMessage);

        IEnumerable<StockResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpGet]
    [SwaggerOperation("Returns a list of materials in stock that expire in the next X days")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<StockResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListBeforeExpire(int days)
    {
        if (days < 1) return BadRequest("Days must be greater than 0");

        var query = new ListStockExpireLimitQuery(days);
        IResult<IEnumerable<Stock>> result = await _mediator.Send(query);

        if (result.IsFailure) return BadRequest(result.ErrorMessage);

        IEnumerable<StockResponse> response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation("Add material to stock")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(StockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StockResponse>> Create(AddStockRequest request)
    {
        if (ModelState.IsValid == false) return BadRequest(ModelState);

        var command = new CreateStockCommand(request.MaterialId, request.ExpireDate, request.BatchDate, request.PositionId);
        IResult<Stock> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        StockResponse response = result.Data!.ToResponse();

        return Ok(response);
    }

    [HttpDelete("{id:long}")]
    [SwaggerOperation("Delete a material from stock")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(StockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StockResponse>> Create(long id)
    {
        if (id < 1) return BadRequest(ModelState);

        var command = new DeleteStockCommand(id);
        IResult<Stock> result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.ErrorMessage);
        }

        StockResponse response = result.Data!.ToResponse();

        return Ok(response);
    }
}