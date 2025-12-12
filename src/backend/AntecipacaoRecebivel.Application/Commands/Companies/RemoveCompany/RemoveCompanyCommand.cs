using AnticipationOfReceivables.BuildingBlocks.Cqrs.Base;
using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;

namespace AnticipationOfReceivables.Application.Commands.Companies.RemoveCompany;

public sealed record RemoveCompanyCommand()
    : Command<EmptyResponse>;
