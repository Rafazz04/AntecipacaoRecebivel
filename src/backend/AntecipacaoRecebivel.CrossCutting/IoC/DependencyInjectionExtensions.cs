using AntecipacaoRecebivel.Infrastructure.DataAcess;
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
	}

	private static void AddServices(IServiceCollection services)
	{
	}
	private static void AddValidators(IServiceCollection services)
	{
	}

}
