using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace AnticipationOfReceivables.API.Configurations.Extensions;

public static class AppBuilderExtensions
{
    public static WebApplication AddConfigurationsApp(this WebApplication app)
    {
        AddSwagger(app);
        AddHttpRedirection(app);
        AddAuthorization(app);
        AddControllers(app);
        AddCHealthChecks_SqlServer(app);
        return app;
    }

    public static WebApplication AddSwagger(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Anticipation Of Receivables API v1");
            });
        }

        return app;
    }
    public static WebApplication AddHttpRedirection(WebApplication app)
    {
        app.UseHttpsRedirection();

        return app;
    }
    public static WebApplication AddAuthorization(WebApplication app)
    {
        app.UseAuthorization();

        return app;
    }
    public static WebApplication AddControllers(WebApplication app)
    {
        app.MapControllers();

        return app;
    }
    public static WebApplication AddCHealthChecks_SqlServer(WebApplication app)
    {
        app.MapHealthChecks("/healthz", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
