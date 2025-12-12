using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;

public sealed record Money
{
    public decimal Value { get; }

    private Money() { } 

    public Money(decimal value, bool allowZero)
    {
        if (!allowZero && value < 0)
            throw new BusinessException(MoneyErrors.NegativeAmountNotAllowed);

        Value = decimal.Round(value, 2);
    }

    public Money(decimal value) : this(value, allowZero: false) { }

    public static Money Zero() => new(0, allowZero: true);

    public static Money operator +(Money a, Money b)
        => new(a.Value + b.Value, allowZero: true);

    public static Money operator -(Money a, Money b)
        => new(a.Value - b.Value, allowZero: true);
}


