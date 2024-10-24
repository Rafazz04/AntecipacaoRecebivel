using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;

namespace AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;

public class CarrinhoCheckoutReadResponseDTO
{
	public string Nome { get; set; }
	public string Cnpj { get; set; }
	public decimal Limite { get; set; }
	public IList<NotaFiscalCreateResponseDTO> NotasFiscais { get; set; }
    public decimal TotalLiquido { get; set; }
    public decimal TotalBruto { get; set; }
}
