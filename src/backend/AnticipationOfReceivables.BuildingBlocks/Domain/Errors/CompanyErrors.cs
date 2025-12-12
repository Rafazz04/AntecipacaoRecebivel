using AnticipationOfReceivables.BuildingBlocks.Exceptions;

namespace AnticipationOfReceivables.BuildingBlocks.Domain.Errors;

public static class CompanyErrors
{
    public static readonly BusinessError CnpjIsRequired =
        new("Company.Cnpj.Required", "O CNPJ é obrigatório.");

    public static readonly BusinessError CnpjInvalidLength =
        new("Company.Cnpj.InvalidLength", "O CNPJ deve conter exatamente 14 dígitos.");

    public static readonly BusinessError CnpjExists =
        new("Company.Cnpj.Exists", "Este CNPJ já esta cadastrado em nosso sistema.");
}
