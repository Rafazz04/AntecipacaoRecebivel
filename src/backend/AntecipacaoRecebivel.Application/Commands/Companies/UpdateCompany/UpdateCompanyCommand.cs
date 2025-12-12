using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;

namespace AnticipationOfReceivables.Application.Commands.Companies.UpdateCompany;

public sealed record UpdateCompanyCommand(
    string Name,
    decimal MonthlyRevenue,
    string BusinessSector
) : Command<UpdateCompanyResponse>;

