using AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;
using FluentValidation;

namespace AntecipacaoRecebivel.Application.Validators.Empresa;

public class EmpresaCreateDTOValidator : AbstractValidator<EmpresaCreateDTO>
{
    public EmpresaCreateDTOValidator()
    {
		RuleFor(e => e.Cnpj).NotEmpty().WithMessage("CNPJ é obrigatório.")
			.Length(14).WithMessage("CNPJ deve ter 14 caracteres.")
			.Matches(@"^\d{14}$").WithMessage("CNPJ deve conter apenas números.");

		RuleFor(e => e.Nome).NotEmpty().WithMessage("Nome da empresa é obrigatório.");

		RuleFor(e => e.Faturamento).GreaterThan(0).WithMessage("Faturamento mensal deve ser maior que zero.");

		RuleFor(e => e.Ramo).NotEmpty().WithMessage("Ramo da empresa é obrigatório.")
			.Must(r => r == "Serviços" || r == "Produtos").WithMessage("Ramo deve ser 'Serviços' ou 'Produtos'!");

	}
}
