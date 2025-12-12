using AnticipationOfReceivables.BuildingBlocks.Cqrs.Queries;
using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Queries.Carts.CalculateAnticipation;

public sealed class CalculateCartAnticipationQueryHandler(IRepositoryBase repository)
    : QueryHandlerBase<CalculateCartAnticipationQuery, CalculateCartAnticipationResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<QueryResponse<CalculateCartAnticipationResponse>> Handle(
        CalculateCartAnticipationQuery request,
        CancellationToken cancellationToken)
    {
        var cartList = await _repository.ListWithIncludesAsync<Cart>(c => c.CompanyId == request.CompanyId, cancellationToken, c => c.Company, c => c.Invoices);

        var cart = cartList.FirstOrDefault();

        if (cart is null)
            throw new BusinessException(CartError.CartNoExixts);

        var invoicesResult = new List<CalculateInvoiceAnticipationResult>();
        var totalGross = Money.Zero();
        var totalNet = Money.Zero();
        var today = DateTime.UtcNow;

        foreach (var invoice in cart.Invoices)
        {
            var net = AnticipationCalculator.CalculateNet(invoice.GrossAmount, invoice.DueDate.Value, today);

            invoicesResult.Add(new CalculateInvoiceAnticipationResult(
                invoice.Number.Value,
                invoice.GrossAmount.Value,
                net.Value
            ));

            totalGross += invoice.GrossAmount;
            totalNet += net;
        }

        var response =  new CalculateCartAnticipationResponse(
            cart.Company.Name,
            cart.Company.Cnpj.Value.ToString(),
            cart.AvailableCreditLimit.Value,
            invoicesResult,
            totalGross.Value,
            totalNet.Value
        );

        return Success(response);
    }
}
