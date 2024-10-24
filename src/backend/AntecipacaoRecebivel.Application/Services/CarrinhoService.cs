using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;
using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using AntecipacaoRecebivel.Communication.Utils;
using AntecipacaoRecebivel.Domain.Entities;
using AntecipacaoRecebivel.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace AntecipacaoRecebivel.Application.Services;

public class CarrinhoService : ICarrinhoService
{
	private readonly ICarrinhoRepository _carrinhoRepository;
	private readonly INotaFiscalRepository _notaFiscalRepository;
	private readonly IEmpresaRepository _empresaRepository;
	private readonly IMapper _mapper;

	public CarrinhoService(ICarrinhoRepository carrinhoRepository,INotaFiscalRepository notaFiscalRepository,IEmpresaRepository empresaRepository,IMapper mapper)
	{
		_carrinhoRepository = carrinhoRepository;
		_notaFiscalRepository = notaFiscalRepository;
		_empresaRepository = empresaRepository;
		_mapper = mapper;
	}

	public CarrinhoCreateResponseDTO CreateCarrinho(string cnpj)
	{
		try
		{
			var empresa = _empresaRepository.GetByCnpj(cnpj) ?? throw new Exception("Empresa não localizada");

			var carrinho = new Carrinho
			{
				EmpresaId = empresa.Id,
				LimiteDeCreditoDisponivel = CalculaLimiteCredito(empresa),
				ValorTotalBruto = 0,
				ValorTotalLiquido = 0,
				NotasFiscais = new List<NotaFiscal>()
			};

			_carrinhoRepository.Create(carrinho);
			if (_carrinhoRepository.SaveChanges())
				return _mapper.Map<CarrinhoCreateResponseDTO>(carrinho);
			throw new Exception("Erro ao adicionar cliente");
		}
		catch (Exception ex) 
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}

	public CarrinhoCreateResponseDTO AdicionaNotaFiscalAoCarrinho(int carrinhoId, int notaFiscalId)
	{
		try
		{
			var carrinho = _carrinhoRepository.GetCarrinhoComEmpresa(carrinhoId) ?? throw new Exception("Carrinho não localizado");
			var notaFiscal = _notaFiscalRepository.GetById(notaFiscalId) ?? throw new Exception("Nota Fiscal não localizada");

			if (carrinho.ValorTotalBruto + notaFiscal.ValorBruto > carrinho.LimiteDeCreditoDisponivel)
				throw new Exception("Valor total das notas fiscais excede o limite de crédito da empresa");

			if(carrinho.NotasFiscais == null)
                carrinho.NotasFiscais = new List<NotaFiscal>();
            carrinho.NotasFiscais.Add(notaFiscal);
			AtualizaValoresDoCarrinho(carrinho);
			_carrinhoRepository.Update(carrinho);

			if(_carrinhoRepository.SaveChanges())
				return _mapper.Map<CarrinhoCreateResponseDTO>(carrinho);
			throw new Exception("Erro ao adicionar nota fiscal no carrinho");
		}
		catch (Exception ex) 
		{	
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}

	public bool DeleteNotaFiscalCarrinho(int carrinhoId, int notaFiscalId)
	{
		try
		{
			var carrinho = _carrinhoRepository.GetCarrinhoComEmpresa(carrinhoId) ?? throw new Exception("Carrinho não localizado");
			var notaFiscal = carrinho.NotasFiscais.FirstOrDefault(nf => nf.Id == notaFiscalId);

			if (notaFiscal == null)
				throw new Exception("Nota Fiscal não encontrada no carrinho");

			carrinho.NotasFiscais.Remove(notaFiscal);
			AtualizaValoresDoCarrinho(carrinho);
			_carrinhoRepository.Update(carrinho);
			if(_carrinhoRepository.SaveChanges())
				return true;
			return false;
		}
		catch (Exception ex)
		{
			throw new Exception($"{ex.Message} - {ex.StackTrace}");
		}
	}

	public CarrinhoCheckoutReadResponseDTO Checkout(int id)
	{
		var carrinho = _carrinhoRepository.GetCarrinhoComEmpresa(id) ?? throw new Exception("Carrinho não localizado");
		AtualizaValoresDoCarrinho(carrinho);

		return new CarrinhoCheckoutReadResponseDTO
		{
			Nome = carrinho.Empresa.Nome,
			Cnpj = carrinho.Empresa.Cnpj,
			Limite = carrinho.LimiteDeCreditoDisponivel,
			NotasFiscais = carrinho.NotasFiscais.Select(nf => new NotaFiscalCreateResponseDTO
			{
				Numero = nf.Numero,
				ValorBruto = nf.ValorBruto,
				ValorLiquido = CalcularValorLiquido(nf)
			}).ToList(),
			TotalBruto = carrinho.ValorTotalBruto,
			TotalLiquido = carrinho.ValorTotalLiquido
		};
	}

	private decimal CalcularValorLiquido(NotaFiscal notaFiscal)
	{
		var prazo = (notaFiscal.DataVencimento - DateTime.Now).Days;
		var taxa = 0.0465m;
		var desagio = notaFiscal.ValorBruto / (decimal)Math.Pow((double)(1 + taxa), prazo / 30.0);
		return notaFiscal.ValorBruto - desagio;
	}


	private void AtualizaValoresDoCarrinho(Carrinho carrinho)
	{
        if (carrinho.NotasFiscais == null)
            carrinho.NotasFiscais = new List<NotaFiscal>(); 
        carrinho.ValorTotalBruto = carrinho.NotasFiscais.Sum(nf => nf.ValorBruto);
		carrinho.ValorTotalLiquido = carrinho.NotasFiscais.Sum(nf => CalcularValorLiquido(nf));
	}

	private decimal CalculaLimiteCredito(Empresa empresa)
	{
		decimal limite = 0;

		if (empresa.Faturamento >= 10000 && empresa.Faturamento <= 50000)
		{
			limite = empresa.Faturamento * 0.5m;
		}
		else if (empresa.Faturamento > 50000 && empresa.Faturamento <= 100000)
		{
			limite = empresa.Ramo == "Serviços" ? empresa.Faturamento * 0.55m : empresa.Faturamento * 0.6m;
		}
		else if (empresa.Faturamento > 100000)
		{
			limite = empresa.Ramo == "Serviços" ? empresa.Faturamento * 0.6m : empresa.Faturamento * 0.65m;
		}

		return limite;
	}

}
