using Bonsai.Backend.Services;

namespace Bonsai.Backend.Configuration;

public static class AppConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWeatherService, WeatherService>();

        services.AddHttpClient<IWeatherService, WeatherService>(httpClient => 
        {
            var apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY");
            httpClient.DefaultRequestHeaders.Add("key", apiKey);
            httpClient.BaseAddress = new Uri("https://api.weatherapi.com/v1/");
        });
    }
}