using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using System.Text.Json.Serialization;

namespace AnticipationOfReceivables.Application.Commands.Carts.AddInvoiceToCart;

public sealed record AddInvoiceToCartCommand()
    : Command<AddInvoiceToCartResponse>
{
    [JsonIgnore]
    public Guid CompanyId { get; set; }

    [JsonIgnore]
    public Guid InvoiceId { get; set; }
}
