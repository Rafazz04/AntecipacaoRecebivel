using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;

namespace AntecipacaoRecebivel.Application.Services.Interfaces;

public interface INotaFiscalService
{
	NotaFiscalCreateResponseDTO GetNotaFiscalById(int id);
	NotaFiscalCreateResponseDTO CreateNotaFiscal(NotaFiscalCreateRequestDTO notaFiscalDto);
	decimal CalcularValorLiquido(int id);
	bool DeletaNotaFiscal(int id);
}
