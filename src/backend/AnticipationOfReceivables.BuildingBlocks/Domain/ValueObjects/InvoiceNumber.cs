using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;
using System.Text.RegularExpressions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;

public sealed record InvoiceNumber
{
    public const int MaxLength = 50;

    public string Value { get; }

    private static readonly Regex ValidFormat =
        new(@"^[A-Za-z0-9\-./]+$", RegexOptions.Compiled);

    public InvoiceNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException(InvoiceErrors.NumberIsRequired);

        var normalized = value.Trim();

        if (normalized.Length > MaxLength)
            throw new BusinessException(InvoiceErrors.NumberTooLong);

        if (!ValidFormat.IsMatch(normalized))
            throw new BusinessException(InvoiceErrors.NumberInvalidFormat);

        Value = normalized;
    }

    public override string ToString() => Value;
}
