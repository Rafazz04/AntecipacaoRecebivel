using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;
using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;
using AnticipationOfReceivables.Domain.Entities;
using AnticipationOfReceivables.Domain.Repository.Contracts;

namespace AnticipationOfReceivables.Application.Commands.Companies.RemoveCompany;

public sealed class RemoveCompanyCommandHandler(IRepositoryBase repository)
    : CommandHandlerBase<RemoveCompanyCommand, EmptyResponse>
{
    private readonly IRepositoryBase _repository = repository;

    public override async Task<CommandResponse<EmptyResponse>> Handle(
        RemoveCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _repository.GetByIdAsync<Company>(request.Id, cancellationToken);

        if (company is null)
            return Failure("COMPANY_NOT_FOUND", "Empresa não encontrada.", "404");

        _repository.Remove(company);

        return Success(EmptyResponse.Instance);
    }
}