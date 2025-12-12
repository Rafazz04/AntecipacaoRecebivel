using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;
using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Commands.Invoices.RemoveInvoice;

public sealed class RemoveInvoiceCommandHandler(IRepositoryBase repository)
    : CommandHandlerBase<RemoveInvoiceCommand, EmptyResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<CommandResponse<EmptyResponse>> Handle(
        RemoveInvoiceCommand request,
        CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync<Invoice>(request.Id, cancellationToken);

        if (invoice is null)
            return Failure("INVOICE_NOT_FOUND", "Fatura não encontrada.", "404");

        _repository.Remove(invoice);

        return Success(EmptyResponse.Instance);
    }
}