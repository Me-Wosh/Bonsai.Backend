using Bonsai.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bonsai.Backend.Endpoints;

public static class WeatherEndpoints
{
    public static void MapWeatherEndpoints(this WebApplication app)
    {
        app.MapGet("v1/weather", async ([FromQuery]float latitude, [FromQuery]float longitude, IWeatherService weatherService) => 
        {
            return await weatherService.GetWeather(latitude, longitude);
        }).RequireRateLimiting("weatherEndpointLimiter");
    }
}