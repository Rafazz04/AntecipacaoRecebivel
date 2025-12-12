using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;

public sealed record DueDate
{
    private DueDate() { }
    public DateTime Value { get; }

    public DueDate(DateTime value, DateTime referenceDate)
    {
        var normalized = value.Date;
        var reference = referenceDate.Date;

        if (normalized <= reference)
            throw new BusinessException(DueDateErros.DueDateMustBeInFuture);

        Value = normalized;
    }

    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
