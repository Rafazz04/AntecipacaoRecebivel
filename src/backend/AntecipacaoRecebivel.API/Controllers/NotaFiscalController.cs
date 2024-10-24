using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebivel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotaFiscalController : ControllerBase
{
	private readonly INotaFiscalService _notaFiscalService;

	public NotaFiscalController(INotaFiscalService notaFiscalService)
	{
		_notaFiscalService = notaFiscalService;
	}

	[HttpPost]
	public ActionResult<NotaFiscalCreateResponseDTO> Post(NotaFiscalCreateRequestDTO requestDTO)
	{
		var nota = _notaFiscalService.CreateNotaFiscal(requestDTO);
		if (nota == null)
			return BadRequest();
		return Ok(nota);
	}
}
