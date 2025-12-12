using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;
using System.Text.RegularExpressions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;

public sealed record Cnpj
{
    public string Value { get; }

    public Cnpj(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException(CompanyErrors.CnpjIsRequired);

        var normalized = Regex.Replace(value, @"\D", "");

        if (normalized.Length != 14)
            throw new BusinessException(CompanyErrors.CnpjInvalidLength);

        Value = normalized;
    }
    public override string ToString() => Value;
}

