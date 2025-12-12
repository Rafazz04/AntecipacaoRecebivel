using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;

namespace AnticipationOfReceivables.Application.Queries.Carts.CalculateAnticipation;

public static class AnticipationCalculator
{
    private const decimal MonthlyRate = 0.0465m;

    public static Money CalculateNet(Money grossAmount, DateTime dueDate, DateTime today)
    {
        var days = (dueDate - today).Days;
        var factor = (decimal)Math.Pow((double)(1 + MonthlyRate), (double)days / 30.0);
        var netValue = grossAmount.Value / factor;
        return new Money(netValue, allowZero: true);
    }
}