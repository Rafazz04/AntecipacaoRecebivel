namespace AntecipacaoRecebivel.Domain.Entities;

public class NotaFiscal : EntityBase
{
	public string Numero { get; set; }
	public decimal ValorBruto { get; set; }
	public DateTime DataVencimento { get; set; }
	public int EmpresaId { get; set; }
	public Empresa Empresa { get; set; }
	public int? CarrinhoId { get; set; }
	public Carrinho Carrinho { get; set; }
}
