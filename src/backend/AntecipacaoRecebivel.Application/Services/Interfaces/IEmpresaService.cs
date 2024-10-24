using AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;

namespace AntecipacaoRecebivel.Application.Services.Interfaces;

public interface IEmpresaService
{
	EmpresaCreateDTO GetEmpresaByCnpj(string cnpj);
	EmpresaCreateDTO CreateEmpresa(EmpresaCreateDTO empresaDto);
	decimal CalcularLimiteAntecipacao(string cnpj); 
	bool DeletaEmpresa(string cnpj);
}
