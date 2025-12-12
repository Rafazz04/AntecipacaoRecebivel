using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;
using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using System.Text.Json.Serialization;

namespace AnticipationOfReceivables.Application.Commands.Invoices.RemoveInvoice;

public sealed record RemoveInvoiceCommand() : Command<EmptyResponse>;