using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using AntecipacaoRecebivel.Domain.Entities;
using AntecipacaoRecebivel.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace AntecipacaoRecebivel.Application.Services;

public class NotaFiscalService : INotaFiscalService
{
	private readonly INotaFiscalRepository _notaFiscalRepository;
	private readonly IMapper _mapper;
	private readonly IValidator<NotaFiscalCreateRequestDTO> _validator;

	public NotaFiscalService(INotaFiscalRepository notaFiscalRepository, IMapper mapper, IValidator<NotaFiscalCreateRequestDTO> validator)
	{
		_notaFiscalRepository = notaFiscalRepository;
		_mapper = mapper;
		_validator = validator;
	}

	public NotaFiscalCreateResponseDTO GetNotaFiscalById(int id)
	{
		var notaFiscal = _notaFiscalRepository.GetById(id) ?? throw new Exception("Nota Fiscal não localizada");
		return _mapper.Map<NotaFiscalCreateResponseDTO>(notaFiscal);
	}

	public NotaFiscalCreateResponseDTO CreateNotaFiscal(NotaFiscalCreateRequestDTO notaFiscalDto)
	{
		try
		{
			if (_validator.Validate(notaFiscalDto).IsValid)
			{
				var notaFiscal = _mapper.Map<NotaFiscal>(notaFiscalDto);
				_notaFiscalRepository.Create(notaFiscal);
				if (_notaFiscalRepository.SaveChanges())
					return _mapper.Map<NotaFiscalCreateResponseDTO>(notaFiscal);

				throw new Exception("Erro ao adicionar nota fiscal");
			}

			var validationErrors = string.Join(", ", _validator.Validate(notaFiscalDto).Errors.Select(e => e.ErrorMessage));
			throw new ValidationException($"Erro de validação: {validationErrors}");
		}
		catch (Exception ex)
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}

	public decimal CalcularValorLiquido(int id)
	{
		var notaFiscal = _notaFiscalRepository.GetById(id) ?? throw new Exception("Nota Fiscal não localizada");

		var prazo = (notaFiscal.DataVencimento - DateTime.Now).Days;
		var taxa = 0.0465m;

		var fator = (double)(1 + taxa);
		var potencia = prazo / 30.0;

		var desagio = notaFiscal.ValorBruto / (decimal)Math.Pow(fator, potencia);
		var valorLiquido = notaFiscal.ValorBruto - desagio;

		return valorLiquido;
	}

	public bool DeletaNotaFiscal(int id)
	{
		try
		{
			var notaFiscal = _notaFiscalRepository.GetById(id) ?? throw new Exception("Nota Fiscal não localizada");
			_notaFiscalRepository.Delete(notaFiscal);
			return _notaFiscalRepository.SaveChanges();
		}
		catch (Exception ex)
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}
}
