using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;
using FluentValidation;

namespace AntecipacaoRecebivel.Application.Validators.NotaFiscal;

public class NotaFiscalCreateRequestDTOValidator : AbstractValidator<NotaFiscalCreateRequestDTO>
{
    public NotaFiscalCreateRequestDTOValidator()
    {
		RuleFor(n => n.Numero).NotEmpty().WithMessage("Número da nota fiscal é obrigatório.");

		RuleFor(n => n.ValorBruto).GreaterThan(0).WithMessage("O valor da nota fiscal deve ser maior que zero.");

		RuleFor(n => n.DataVencimento).GreaterThan(DateTime.Today).WithMessage("A data de vencimento deve ser maior que o dia de hoje.");
	}
}
