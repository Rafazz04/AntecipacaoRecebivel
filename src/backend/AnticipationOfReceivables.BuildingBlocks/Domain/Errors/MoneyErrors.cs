using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.Errors;

public static class MoneyErrors
{
    public static readonly BusinessError NegativeAmountNotAllowed =
        new("Money.NegativeAmount", "O valor monetário não pode ser negativo.");
}
