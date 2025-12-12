using AnticipationOfReceivables.API.Controllers.Base;
using AnticipationOfReceivables.Application.Commands.Companies.CreateCompany;
using AnticipationOfReceivables.Application.Commands.Companies.RemoveCompany;
using AnticipationOfReceivables.Application.Commands.Companies.UpdateCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnticipationOfReceivables.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return HandleCreate(result);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        command = command with { Id = id };

        var result = await _mediator.Send(command, cancellationToken);
        return HandleUpdate(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new RemoveCompanyCommand { Id = id };

        var result = await _mediator.Send(command, cancellationToken);
        return HandleDelete(result);
    }
}
