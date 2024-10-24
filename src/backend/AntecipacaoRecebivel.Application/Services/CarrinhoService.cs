using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;
using AntecipacaoRecebivel.Application.Services.Interfaces;

namespace AntecipacaoRecebivel.Application.Services;

public class CarrinhoService : ICarrinhoService
{
	public CarrinhoCreateResponseDTO AdicionaNotaFiscalAoCarrinho(int carrinhoId, int notaFiscalId)
	{
		throw new NotImplementedException();
	}

	public CarrinhoCreateResponseDTO CalcularAntecipacaoAsync(int id)
	{
		throw new NotImplementedException();
	}

	public CarrinhoCheckoutReadResponseDTO Checkout(int id)
	{
		throw new NotImplementedException();
	}

	public CarrinhoCreateResponseDTO CreateCarrinho(string cnpj)
	{
		throw new NotImplementedException();
	}

	public bool DeleteCarrinho(int carrinhoId, int notaFiscalId)
	{
		throw new NotImplementedException();
	}
}
