using Bonsai.Backend.Configuration;
using Bonsai.Backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

var app = builder.Build();

app.UsePathBase("/api");

app.MapWeatherEndpoints();

app.Run();