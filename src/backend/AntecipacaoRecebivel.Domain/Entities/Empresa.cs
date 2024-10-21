namespace AntecipacaoRecebivel.Domain.Entities;

public class Empresa : EntityBase
{
	public string Cnpj { get; set; }
	public string Nome { get; set; }
	public decimal Faturamento { get; set; }
	public string Ramo { get; set; } 
	public decimal LimiteCredito { get; set; }
	public ICollection<NotaFiscal> NotasFiscais { get; set; }
	public ICollection<Carrinho> Carrinhos { get; set; }
}
