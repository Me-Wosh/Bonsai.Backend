using Bonsai.Backend.Configuration;
using Bonsai.Backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder);

var app = builder.Build();

app.UsePathBase("/api");

app.UseRateLimiter();

app.MapWeatherEndpoints();

app.Run();