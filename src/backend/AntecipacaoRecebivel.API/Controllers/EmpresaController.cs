using AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebivel.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpresaController : ControllerBase
{
	private readonly IEmpresaService _empresaService;

	public EmpresaController(IEmpresaService empresaService)
	{
		_empresaService = empresaService;
	}

	[HttpPost]
	public ActionResult<EmpresaCreateDTO> Post(EmpresaCreateDTO empresaCreateDTO)
	{
		var empresa = _empresaService.CreateEmpresa(empresaCreateDTO);
		if (empresa == null)
			return BadRequest();
		return Ok(empresa);	
	}
}
