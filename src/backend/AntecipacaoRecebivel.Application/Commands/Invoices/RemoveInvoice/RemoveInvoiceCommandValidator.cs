using FluentValidation;

namespace AnticipationOfReceivables.Application.Commands.Invoices.RemoveInvoice;

public sealed class RemoveInvoiceCommandValidator : AbstractValidator<RemoveInvoiceCommand>
{
    public RemoveInvoiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O Id da nota fiscal a ser removida é obrigatória.");
    }
}
