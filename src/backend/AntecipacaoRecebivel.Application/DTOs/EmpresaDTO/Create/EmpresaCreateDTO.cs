namespace AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;

public class EmpresaCreateDTO
{
    public string Cnpj { get; set; }
    public string Nome { get; set; }
    public decimal Faturamento { get; set; }
    public string Ramo { get; set; }
}
