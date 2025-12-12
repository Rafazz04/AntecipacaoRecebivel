using FluentValidation;

namespace AnticipationOfReceivables.Application.Commands.Companies.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Cnpj)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.MonthlyRevenue)
            .GreaterThan(0);

        RuleFor(x => x.BusinessSector)
            .NotEmpty();
    }
}