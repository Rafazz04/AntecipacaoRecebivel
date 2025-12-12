using AnticipationOfReceivables.API.Controllers.Base;
using AnticipationOfReceivables.Application.Commands.Carts.AddInvoiceToCart;
using AnticipationOfReceivables.Application.Queries.Carts.CalculateAnticipation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnticipationOfReceivables.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost("{companyId}/invoices/{invoiceId}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddInvoiceToCart(
        [FromRoute] Guid companyId,
        [FromRoute] Guid invoiceId,
        CancellationToken cancellationToken)
    {
        var command = new AddInvoiceToCartCommand { CompanyId = companyId, InvoiceId = invoiceId };

        var result = await _mediator.Send(command, cancellationToken);
        return HandleCreate(result);
    }


    [HttpGet("{companyId}/checkout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Checkout(Guid companyId, CancellationToken cancellationToken)
    {
        var query = new CalculateCartAnticipationQuery(companyId);
        var result = await _mediator.Send(query, cancellationToken);
        return HandleGet(result); 
    }
}
