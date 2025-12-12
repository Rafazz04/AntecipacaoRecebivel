using FluentValidation;

namespace AnticipationOfReceivables.Application.Commands.Invoices.CreateInvoice;

public sealed class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(invoice => invoice.Number)
            .NotEmpty().WithMessage("O Número da nota é obrigatório.");

        RuleFor(invoice => invoice.Value)
            .GreaterThan(0).WithMessage("O Valor deve ser maior que zero.");

        RuleFor(invoice => invoice.DueDate)
            .GreaterThan(DateTime.UtcNow.Date)
            .WithMessage("A data de vencimento deve ser maior que hoje.");

        RuleFor(invoice => invoice.CompanyId)
            .NotEmpty().WithMessage("A Empresa é obrigatória.");
    }
}
