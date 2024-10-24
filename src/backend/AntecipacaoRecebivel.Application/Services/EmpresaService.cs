using AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using AntecipacaoRecebivel.Communication.Utils;
using AntecipacaoRecebivel.Domain.Entities;
using AntecipacaoRecebivel.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace AntecipacaoRecebivel.Application.Services;

public class EmpresaService : IEmpresaService
{
	private readonly IEmpresaRepository _empresaRepository;
	private readonly IMapper _mapper;
	private readonly IValidator<EmpresaCreateDTO> _validator;

	public EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper, IValidator<EmpresaCreateDTO> validator)
	{
		_empresaRepository = empresaRepository;
		_mapper = mapper;
		_validator = validator;
	}
	public EmpresaCreateDTO GetEmpresaByCnpj(string cnpj)
	{
		var empresa = _empresaRepository.GetByCnpj(Util.LimpaCnpj(cnpj));
		return _mapper.Map<EmpresaCreateDTO>(empresa);
	}
	public EmpresaCreateDTO CreateEmpresa(EmpresaCreateDTO empresaDto)
	{
		try
		{
			if (_validator.Validate(empresaDto).IsValid)
			{
				var empresa = _mapper.Map<Empresa>(empresaDto);
				_empresaRepository.Create(empresa);
				if (_empresaRepository.SaveChanges())
					return empresaDto;
				throw new Exception("Erro ao adicionar cliente");
			}
			var validationErrors = string.Join(", ", _validator.Validate(empresaDto).Errors.Select(e => e.ErrorMessage));
			throw new ValidationException($"Erro de validação: {validationErrors}");
		}
		catch (Exception ex)
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}

	public decimal CalcularLimiteAntecipacao(string cnpj)
	{
		var empresa = _empresaRepository.GetByCnpj(Util.LimpaCnpj(cnpj)) ?? throw new Exception("Empresa Não localizada");

		decimal limite = 0;
		var faturamento = empresa.Faturamento;

		if (faturamento >= 10000 && faturamento <= 50000)
		{
			limite = faturamento * 0.50m;
		}
		else if (faturamento >= 50001 && faturamento <= 100000)
		{
			limite = empresa.Ramo == "Serviços" ? faturamento * 0.55m : faturamento * 0.60m;
		}
		else if (faturamento > 100000)
		{
			limite = empresa.Ramo == "Serviços" ? faturamento * 0.60m : faturamento * 0.65m;
		}

		return limite;
	}
	public bool DeletaEmpresa(string cnpj)
	{
		try
		{
			var empresa = _empresaRepository.GetByCnpj(Util.LimpaCnpj(cnpj)) ?? throw new Exception("Empresa Não localizada");
			_empresaRepository.Delete(empresa);
			if (_empresaRepository.SaveChanges())
				return true;
			return false;
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}
}
