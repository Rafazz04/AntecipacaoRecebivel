using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;
using FluentValidation;

namespace AntecipacaoRecebivel.Application.Validators.Carrinho;

public class CarrinhoCheckoutReadResponseDTOValidator : AbstractValidator<CarrinhoCheckoutReadResponseDTO>
{
    public CarrinhoCheckoutReadResponseDTOValidator()
    {
		RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome da empresa é obrigatório.");

		RuleFor(c => c.Cnpj).NotEmpty().WithMessage("CNPJ é obrigatório.")
			.Length(14).WithMessage("CNPJ deve ter 14 caracteres.")
			.Matches(@"^\d{14}$").WithMessage("CNPJ deve conter apenas números.");

		RuleFor(c => c.Limite).GreaterThan(0).WithMessage("O limite deve ser maior que zero.");

		RuleFor(c => c.TotalBruto).GreaterThan(0).WithMessage("O total bruto deve ser maior que zero.");

		RuleFor(c => c.TotalLiquido).GreaterThan(0).WithMessage("O total líquido deve ser maior que zero.");
	}
}
