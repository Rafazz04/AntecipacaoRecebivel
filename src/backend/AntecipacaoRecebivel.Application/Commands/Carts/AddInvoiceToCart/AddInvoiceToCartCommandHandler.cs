using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Commands.Carts.AddInvoiceToCart;

public sealed class AddInvoiceToCartCommandHandler(IRepositoryBase repository)
    : CommandHandlerBase<AddInvoiceToCartCommand, AddInvoiceToCartResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<CommandResponse<AddInvoiceToCartResponse>> Handle(
        AddInvoiceToCartCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync<Company>(request.CompanyId, cancellationToken);
        if (company is null)
            return Failure("ERROR_COMPANY_NOT_FOUND", "Empresa não encontrada.", "404");

        var invoice = await _repository.GetByIdAsync<Invoice>(request.InvoiceId, cancellationToken);
        if (invoice is null || invoice.CompanyId != company.Id)
            return Failure("ERROR_INVOICE_NOT_FOUND", "Nota fiscal não encontrada para esta empresa.", "404");

        var cart = await _repository.FirstOrDefaultAsync<Cart>(
            q => q.Where(c => c.CompanyId == company.Id),
            cancellationToken
        );

        if (cart is null)
        {
            cart = new Cart(company, company.CreditLimit);
            cart.AddInvoice(invoice);
            await _repository.AddAsync(cart, cancellationToken);
        }
        else
        {
            cart.AddInvoice(invoice);
        }

        return Success(new AddInvoiceToCartResponse(cart.Id));
    }
}


