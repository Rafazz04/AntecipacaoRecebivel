namespace AntecipacaoRecebivel.Domain.Entities;

public class EntityBase
{
	public int Id { get; set; }
	public DateTime DataCadastro { get; set; } = DateTime.Now;
	public DateTime DataAtualiza { get; set; } = DateTime.Now;
}
