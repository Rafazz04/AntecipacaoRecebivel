using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.BuildingBlocks.Domain.Errors;
using AnticipationOfReceivables.BuildingBlocks.Domain.ValueObjects;
using AnticipationOfReceivables.BuildingBlocks.Exceptions;
using AnticipationOfReceivables.BuildingBlocks.Util;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Entities.Enums;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Commands.Companies.CreateCompany;

public sealed class CreateCompanyCommandHandler(IRepositoryBase repository) : CommandHandlerBase<CreateCompanyCommand, CreateCompanyResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<CommandResponse<CreateCompanyResponse>> Handle(
    CreateCompanyCommand request,
    CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<BusinessSector>(
                request.BusinessSector.ToUpperInvariant(),
                ignoreCase: true,
                out var businessSector))
        {
            throw new BusinessException(BusinessSectorError.ServicesOrProducts);
        }

        var cnpjExists = await _repository.AnyAsync<Company>(
            query => query.Where(c => c.Cnpj.Value.Equals(Util.ClearCnpj(request.Cnpj))),
            cancellationToken
        );

        if (cnpjExists)
            return Failure("ERROR_COMPANY", "Este CNPJ ja existe em nosso sistema.", "400");

        var company = new Company(  
            new Cnpj(request.Cnpj),
            request.Name,
            businessSector,         
            new Money(request.MonthlyRevenue)
        );

        await _repository.AddAsync(company, cancellationToken);

        return Success(new CreateCompanyResponse(company.Id));
    }

}
