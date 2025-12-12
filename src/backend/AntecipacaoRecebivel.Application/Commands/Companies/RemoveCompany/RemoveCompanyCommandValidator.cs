using FluentValidation;

namespace AnticipationOfReceivables.Application.Commands.Companies.RemoveCompany;

public sealed class RemoveCompanyCommandValidator : AbstractValidator<RemoveCompanyCommand>
{
    public RemoveCompanyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O Id da empresa é obrigatório.");
    }
}
