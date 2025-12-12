using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Commands.Invoices.CreateInvoice;

public sealed class CreateInvoiceCommandHandler(IRepositoryBase repository)
    : CommandHandlerBase<CreateInvoiceCommand, CreateInvoiceResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<CommandResponse<CreateInvoiceResponse>> Handle(
        CreateInvoiceCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync<Company>(request.CompanyId, cancellationToken);

        if (company is null)
            return Failure("COMPANY_NOT_FOUND", "Empresa não encontrada.", "404");

        var invoice = new Invoice(
            new InvoiceNumber(request.Number),
            new Money(request.Value),
            new DueDate(request.DueDate, DateTime.UtcNow),
            company
        );
        company.InvoicesAdd(invoice);

        await _repository.AddAsync(invoice, cancellationToken);

        return Success(new CreateInvoiceResponse(invoice.Id));
    }
}
