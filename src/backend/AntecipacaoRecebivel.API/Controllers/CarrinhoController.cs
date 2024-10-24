using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebivel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarrinhoController : ControllerBase
{
	private readonly ICarrinhoService _carrinhoService;

	public CarrinhoController(ICarrinhoService carrinhoService)
	{
		_carrinhoService = carrinhoService;
	}

	[HttpPost("Empresa")]
	public ActionResult<CarrinhoCreateResponseDTO> Post(string cnpj)
	{
		var carrinho = _carrinhoService.CreateCarrinho(cnpj);
		if (carrinho == null)
			return BadRequest();
		return Ok(carrinho);
	}

	[HttpPost("NotaFiscal")]
	public ActionResult<CarrinhoCreateResponseDTO> AdicionaNotaFiscal(int carrinhoId, int notaFiscalId)
	{
		var carrinho = _carrinhoService.AdicionaNotaFiscalAoCarrinho(carrinhoId, notaFiscalId);
		if (carrinho == null)
			return BadRequest();
		return Ok(carrinho);
	}

	[HttpGet("checkout")]
	public ActionResult<CarrinhoCheckoutReadResponseDTO> Checkout(int id)
	{
		try
		{
			var checkoutResponse = _carrinhoService.Checkout(id);
			return Ok(checkoutResponse);
		}
		catch (Exception ex)
		{
			return NotFound(ex.Message);
		}
	}

}
