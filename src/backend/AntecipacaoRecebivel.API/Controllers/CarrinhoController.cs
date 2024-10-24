using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
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

	[HttpPost]
	public ActionResult<CarrinhoCreateResponseDTO> Post(string cnpj)
	{
		var carrinho = _carrinhoService.CreateCarrinho(cnpj);
		if (carrinho == null)
			return BadRequest();
		return Ok(carrinho);
	}
}
