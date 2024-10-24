namespace AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;

public class NotaFiscalCreateResponseDTO
{
	public string Numero { get; set; }
	public decimal ValorBruto { get; set; }
	public decimal ValorLiquido { get; set; }

}
