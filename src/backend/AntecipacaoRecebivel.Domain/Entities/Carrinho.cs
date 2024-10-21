namespace AntecipacaoRecebivel.Domain.Entities;

public class Carrinho : EntityBase
{
	public decimal ValorTotalBruto { get; set; }
	public decimal ValorTotalLiquido { get; set; }
	public decimal LimiteDeCreditoDisponivel { get; set; }
	public int EmpresaId { get; set; }
	public Empresa Empresa { get; set; }
	public ICollection<NotaFiscal> NotasFiscais { get; set; }
}
