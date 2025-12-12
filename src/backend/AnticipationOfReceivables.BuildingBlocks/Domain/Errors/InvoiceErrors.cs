using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.Errors;

public static class InvoiceErrors
{
    public static readonly BusinessError NumberIsRequired =
        new("Invoice.Number.Required", "O número da nota fiscal é obrigatório.");

    public static readonly BusinessError NumberTooLong =
        new("Invoice.Number.TooLong", "O número da nota fiscal deve ter no máximo 50 caracteres.");

    public static readonly BusinessError NumberInvalidFormat =
        new("Invoice.Number.InvalidFormat", "O número da nota fiscal possui caracteres inválidos.");
}
