using AnticipationOfReceivables.API.Configurations.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationsServices(builder.Configuration);

var app = builder.Build();

app.AddConfigurationsApp();

app.Run();