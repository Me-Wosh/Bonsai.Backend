using System.Globalization;
using Bonsai.Backend.Converters;
using Bonsai.Backend.Models;

namespace Bonsai.Backend.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IResult> GetWeather(float latitude, float longitude)
    {
        var (isLocationValid, locationValidationErrorMessage) = IsLocationValid(latitude, longitude);

        if (!isLocationValid)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Location validation error",
                detail: locationValidationErrorMessage
            );
        }

        var lat = latitude.ToString(CultureInfo.InvariantCulture);
        var lon = longitude.ToString(CultureInfo.InvariantCulture);

        var response = await _httpClient.GetAsync($"current.json?q={lat},{lon}");
        
        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();

            return Results.Problem(
                statusCode: (int)response.StatusCode, 
                detail: errorMessage
            );
        }

        var content = await response.Content.ReadFromJsonAsync<WeatherResponse>();

        if (content == null)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status502BadGateway, 
                title: "Null response", 
                detail: $"Expected: {typeof(WeatherResponse)}, got null instead."
            );
        }

        var weather = WeatherConverter.Convert(content.Current.Condition.Text);

        return Results.Ok(weather);
    }

    private (bool, string) IsLocationValid(float latitude, float longitude)
    {
        var errorMessage = string.Empty;

        if (latitude is < -90 or > 90)
        {
            errorMessage += $"Expected a latitude value between -90 and 90, got {latitude} instead.";
        }

        if (longitude is < -180 or > 180)
        {
            errorMessage += $" Expected a longitude value between -180 and 180, got {longitude} instead.";
        }

        if (errorMessage == string.Empty)
        {
            return (true, errorMessage);
        }

        return (false, errorMessage.Trim());
    }
}