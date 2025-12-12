using FluentValidation;

namespace AnticipationOfReceivables.Application.Queries.Carts.CalculateAnticipation;

public sealed class CalculateCartAnticipationQueryValidator : AbstractValidator<CalculateCartAnticipationQuery>
{
    public CalculateCartAnticipationQueryValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
    }
}
