using AntecipacaoRecebivel.Application.DTOs.CarrinhoDTO.Read;
using AntecipacaoRecebivel.Application.DTOs.EmpresaDTO.Create;
using AntecipacaoRecebivel.Application.DTOs.NotaFiscalDTO.Create;
using AntecipacaoRecebivel.Application.Mappings;
using AntecipacaoRecebivel.Application.Services;
using AntecipacaoRecebivel.Application.Services.Interfaces;
using AntecipacaoRecebivel.Application.Validators.Carrinho;
using AntecipacaoRecebivel.Application.Validators.Empresa;
using AntecipacaoRecebivel.Application.Validators.NotaFiscal;
using AntecipacaoRecebivel.Domain.Interfaces;
using AntecipacaoRecebivel.Infrastructure.DataAcess;
using AntecipacaoRecebivel.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AntecipacaoRecebivel.CrossCutting.IoC;

public static class DependencyInjectionExtensions
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		AddDbContext_SqlServer(services, configuration);
		AddRepositories(services);
	}
	public static void AddApplication(this IServiceCollection services)
	{
		AddServices(services);
		AddValidators(services);
	}
	private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
	{
		var connectiontring = configuration.GetConnectionString("DefaultConnection");
		services.AddDbContext<AntecipacaoRecebiveisDbContext>(ctx =>
		{
			ctx.UseSqlServer(connectiontring);
		});
	}
	private static void AddRepositories(IServiceCollection services)
	{
		services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
		services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
		services.AddScoped<IEmpresaRepository, EmpresaRepository>();
		services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
	}

	private static void AddServices(IServiceCollection services)
	{
		services.AddScoped<ICarrinhoService, CarrinhoService>();
		services.AddScoped<IEmpresaService, EmpresaService>();
		services.AddScoped<INotaFiscalService, NotaFiscalService>();
		services.AddAutoMapper(typeof(AntecipacaoRecebivelMappingProfile));
	}
	private static void AddValidators(IServiceCollection services)
	{
		services.AddScoped<IValidator<EmpresaCreateDTO>, EmpresaCreateDTOValidator>();
		services.AddScoped<IValidator<NotaFiscalCreateRequestDTO>, NotaFiscalCreateRequestDTOValidator>();
		services.AddScoped<IValidator<CarrinhoCheckoutReadResponseDTO>, CarrinhoCheckoutReadResponseDTOValidator>();
	}

}
