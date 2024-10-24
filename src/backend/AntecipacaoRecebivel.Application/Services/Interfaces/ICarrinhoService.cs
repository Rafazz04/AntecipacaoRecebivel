using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;

namespace AntecipacaoRecebivel.Application.Services.Interfaces;

public interface ICarrinhoService
{
	CarrinhoCreateResponseDTO CreateCarrinho(string cnpj);
	CarrinhoCreateResponseDTO AdicionaNotaFiscalAoCarrinho(int carrinhoId, int notaFiscalId);
	bool DeleteCarrinho(int carrinhoId, int notaFiscalId);
	CarrinhoCreateResponseDTO CalcularAntecipacaoAsync(int id);
	CarrinhoCheckoutReadResponseDTO Checkout(int id);

}
