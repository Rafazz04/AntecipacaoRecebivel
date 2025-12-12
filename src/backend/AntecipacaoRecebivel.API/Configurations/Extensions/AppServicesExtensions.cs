using AnticipationOfReceivables.API.Configurations.Pipelines;
using AnticipationOfReceivables.Application;
using AnticipationOfReceivables.Application.Commands.Companies.CreateCompany;
using AnticipationOfReceivables.Domain.Repository.Contracts;
using AnticipationOfReceivables.Infrastructure.DataAcess;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;


namespace AnticipationOfReceivables.API.Configurations.Extensions;

public static class AppServicesExtensions
{
    public static IServiceCollection AddConfigurationsServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddPipelines(services);
        AddMediatR(services);
        AddLowerCaseController(services);
        AddRepository(services);
        AddValidators(services);
        AddDbContext_SqlServer(services, configuration);
        AddHealthChecks(services, configuration);
        AddSwagger(services);
        services.AddAuthorization();
        services.AddControllers();


        return services;
    }

    public static IServiceCollection AddPipelines(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }

    public static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IAnticipationOfReceivablesApplicationEntryPoint).Assembly));

        return services;
    }

    public static IServiceCollection AddLowerCaseController(this IServiceCollection services)
    {
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryBase, AnticipationOfReceivablesDbContext>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[]
        {
            typeof(CreateCompanyCommandValidator).Assembly
        });


        return services;
    }

    public static IServiceCollection AddDbContext_SqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServerDbContext<AnticipationOfReceivablesDbContext>(configuration);

        return services;
    }

    public static void AddSqlServerDbContext<TContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TContext).Assembly.FullName);

                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null
                    );
                }
            )
        );
    }

    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddSqlServer(
                connectionString: configuration.GetConnectionString("DefaultConnection")!,
                name: "SQL Server",
                tags: ["db", "sql"]
            );
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Anticipation of Receivables API",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Name = "Rafael",
                    Email = "Rafael.luz04@outlook.com"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            if (File.Exists(xmlPath))
                options.IncludeXmlComments(xmlPath);
        });

        return services;
    }

}
