using Bonsai.Backend.Services;
using Microsoft.AspNetCore.RateLimiting;

namespace Bonsai.Backend.Configuration;

public static class AppConfiguration
{
    public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddScoped<IWeatherService, WeatherService>();

        services.AddHttpClient<IWeatherService, WeatherService>(httpClient => 
        {
            var apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
            var apiUri = builder.Configuration.GetRequiredSection("WeatherApiUri").Value!;

            httpClient.DefaultRequestHeaders.Add("key", apiKey);
            httpClient.BaseAddress = new Uri(apiUri);
        });

        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.AddFixedWindowLimiter("weatherEndpointLimiter", options =>
            {
                var section = builder.Configuration.GetRequiredSection("WeatherEndpointLimiter"); 
                var window = section.GetValue<int>("Window");
                var permitLimit = section.GetValue<int>("PermitLimit");

                options.Window = TimeSpan.FromSeconds(window);
                options.PermitLimit = permitLimit;
            });
        });
    }
}