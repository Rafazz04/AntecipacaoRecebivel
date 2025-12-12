using FluentValidation;

namespace AnticipationOfReceivables.Application.Commands.Companies.UpdateCompany;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
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