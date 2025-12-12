using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using System.Text.Json.Serialization;

namespace AnticipationOfReceivables.Application.Commands.Invoices.CreateInvoice;

public sealed record CreateInvoiceCommand(
    string Number,
    decimal Value,
    DateTime DueDate
) : Command<CreateInvoiceResponse>
{
    [JsonIgnore]
    public Guid CompanyId { get; set; }
}
