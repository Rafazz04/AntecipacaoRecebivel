namespace AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;

public class NotaFiscalCreateRequestDTO
{
    public string Numero { get; set; }
    public decimal ValorBruto { get; set; }
    public DateTime DataVencimento { get; set; }
    public string Cnpj { get; set; }
}
