using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Entities.Enums;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Commands.Companies.UpdateCompany;

public sealed class UpdateCompanyCommandHandler(IRepositoryBase repository)
    : CommandHandlerBase<UpdateCompanyCommand, UpdateCompanyResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<CommandResponse<UpdateCompanyResponse>> Handle(
        UpdateCompanyCommand request,
        CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<BusinessSector>(
                request.BusinessSector.ToUpperInvariant(),
                ignoreCase: true,
                out var businessSector))
        {
            throw new BusinessException(BusinessSectorError.ServicesOrProducts);
        }

        var company = await _repository.FirstOrDefaultAsync<Company>(
            query => query.Where(c => c.Id == request.Id),
            cancellationToken);

        if (company is null)
            return Failure("ERROR_COMPANY_NOT_FOUND", "Empresa não encontrada.", "404");

        company.Update(
            request.Name,
            businessSector,
            new Money(request.MonthlyRevenue)
        );

        _repository.Update(company);

        return Success(new UpdateCompanyResponse(company.Id));
    }
}
