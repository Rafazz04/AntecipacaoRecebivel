using AnticipationOfReceivables.API.Controllers.Base;
using AnticipationOfReceivables.Application.Commands.Invoices.CreateInvoice;
using AnticipationOfReceivables.Application.Commands.Invoices.RemoveInvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnticipationOfReceivables.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("{companyId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateInvoice(
        [FromRoute]Guid companyId,
        [FromBody] CreateInvoiceCommand command,
        CancellationToken cancellationToken)
    {
        command = command with { CompanyId = companyId };

        var result = await _mediator.Send(command, cancellationToken);
        return HandleCreate(result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new RemoveInvoiceCommand { Id = id };

        var result = await _mediator.Send(command, cancellationToken);
        return HandleDelete(result);
    }
}
