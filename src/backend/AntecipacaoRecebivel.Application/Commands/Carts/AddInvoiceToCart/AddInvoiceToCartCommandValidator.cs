using FluentValidation;

namespace AnticipationOfReceivables.Application.Commands.Carts.AddInvoiceToCart;

public class AddInvoiceToCartCommandValidator : AbstractValidator<AddInvoiceToCartCommand>
{
    public AddInvoiceToCartCommandValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.InvoiceId).NotEmpty();
    }
}