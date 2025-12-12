using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.Errors;

public static class BusinessSectorError
{
    public static readonly BusinessError ServicesOrProducts =
        new("Company.BusinessSector.Invalid", "Ramo inválido. Valores permitidos: Serviços ou Produtos.");
}
