using Bonsai.Backend.Configuration;
using Bonsai.Backend.Endpoints;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UsePathBase("/api");
app.UseRateLimiter();
app.MapWeatherEndpoints();

app.Run();