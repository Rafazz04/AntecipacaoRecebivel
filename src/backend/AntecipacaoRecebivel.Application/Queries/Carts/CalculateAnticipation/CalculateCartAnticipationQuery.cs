using AnticipationOfReceivables.BuildingBlocks.Cqrs.Queries;

namespace AnticipationOfReceivables.Application.Queries.Carts.CalculateAnticipation;

public sealed record CalculateCartAnticipationQuery(Guid CompanyId) : Query<CalculateCartAnticipationResponse>;

public sealed record CalculateCartAnticipationResponse(
    string CompanyName,
    string Cnpj,
    decimal CreditLimit,
    List<CalculateInvoiceAnticipationResult> Invoices,
    decimal TotalGross,
    decimal TotalNet
);

public sealed record CalculateInvoiceAnticipationResult(
    string InvoiceNumber,
    decimal GrossAmount,
    decimal NetAmount
);

