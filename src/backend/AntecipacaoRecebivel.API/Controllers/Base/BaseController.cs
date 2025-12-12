using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;
using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.BuildingBlocks.Cqrs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnticipationOfReceivables.API.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator _mediator = mediator;

    protected IActionResult HandleCreate<TData>(
        CommandResponse<TData> response,
        string? routeName = null,
        object? routeValues = null)
    {
        if (!response.Success)
            return HandleFailure(response);

        if (!string.IsNullOrWhiteSpace(routeName))
            return CreatedAtRoute(routeName, routeValues, response);

        return StatusCode(StatusCodes.Status201Created, response);
    }
    protected IActionResult HandleUpdate<TData>(CommandResponse<TData> response)
    {
        if (!response.Success)
            return HandleFailure(response);

        return Ok(response);
    }
    protected IActionResult HandleDelete(CommandResponse<EmptyResponse> response)
    {
        if (!response.Success)
            return HandleFailure(response);

        return NoContent();
    }
    protected IActionResult HandleGet<TData>(QueryResponse<TData> response)
    {
        if (!response.Success)
        {
            if (response.StatusCode == "404")
                return NotFound(response);

            return HandleFailureQuery(response);
        }

        return Ok(response);
    }

    private IActionResult HandleFailure<TData>(CommandResponse<TData> response)
    {
        var statusCode = int.TryParse(response.StatusCode, out var code)
            ? code
            : StatusCodes.Status400BadRequest;

        return StatusCode(statusCode, response);
    }

    private IActionResult HandleFailureQuery<TData>(QueryResponse<TData> response)
    {
        var statusCode = int.TryParse(response.StatusCode, out var code)
            ? code
            : StatusCodes.Status400BadRequest;

        return StatusCode(statusCode, response);
    }
}
