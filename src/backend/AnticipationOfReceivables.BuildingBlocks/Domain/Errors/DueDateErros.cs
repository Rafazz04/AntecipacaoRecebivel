using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.Errors;

public static class DueDateErros
{
    public static readonly BusinessError DueDateMustBeInFuture =
        new("Invoice.DueDate.MustBeInFuture",
            "A data de vencimento deve ser maior que a data atual.");
}
