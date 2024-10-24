using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;

namespace AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;

public class CarrinhoCreateResponseDTO
{
    public string Cnpj { get; set; }
    public decimal LimiteCredito { get; set; }
    public IList<NotaFiscalCreateResponseDTO> NotaFiscal {  get; set; }
}
