using AnticipationOfReceivables.BuildingBlocks.Cqrs.Commands;

namespace AnticipationOfReceivables.Application.Commands.Companies.CreateCompany;

public sealed record CreateCompanyCommand(
    string Cnpj,
    string Name,
    decimal MonthlyRevenue,
    string BusinessSector
) : Command<CreateCompanyResponse>;
