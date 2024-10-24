using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;

namespace AntecipacaoRecebivel.Application.Services.Interfaces;

public interface ICarrinhoService
{
	CarrinhoCheckoutReadResponseDTO GetCarrinhoById(int id);
	CarrinhoCreateResponseDTO CreateCarrinho(string cnpj);
	CarrinhoCreateResponseDTO AdicionaNotaFiscalAoCarrinho(int carrinhoId, int notaFiscalId);
	bool DeleteNotaFiscalCarrinho(int carrinhoId, int notaFiscalId);
	CarrinhoCreateResponseDTO CalcularAntecipacao(int id);
	CarrinhoCheckoutReadResponseDTO Checkout(int id);

}
