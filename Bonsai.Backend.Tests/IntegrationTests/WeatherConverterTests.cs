using System.Net.Http.Json;
using System.Text.Json;
using Bonsai.Backend.Converters;

namespace IntegrationTests;

public class WeatherConverterTests
{
    [Fact]
    public async Task Convert_ShouldReturnCorrectWeather_IfWeatherRecognized()
    {
        // Arrange
        var weatherConditions = await GetAllPossibleWeatherCondtions();
        var expectedConditions = await GetExpectedWeatherConditions();
        
        // Act
        var day = new List<string>(weatherConditions.Count);
        var night = new List<string>(weatherConditions.Count);

        foreach (var condition in weatherConditions)
        {
            day.Add(WeatherConverter.Convert(condition.Day));
            night.Add(WeatherConverter.Convert(condition.Night));
        }

        // Assert
        Assert.NotEmpty(day);
        Assert.NotEmpty(night);
        Assert.Equal(day, night);
        Assert.Equal(day, expectedConditions);
    }

    [Theory]
    [InlineData("uga buga")]
    [InlineData("")]
    public void Convert_ShouldThrowArgumentException_IfWeatherNotRecognized(string weather)
    {
        // Act
        var action = () => WeatherConverter.Convert(weather);
        
        // Assert
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.NotNull(exception);
        Assert.Equal($"Unknown weather \"{weather}\"", exception.Message);
    }

    private async Task<List<WeatherCondition>> GetAllPossibleWeatherCondtions()
    {
        var httpClient = new HttpClient();
        var uri = "https://www.weatherapi.com/docs/weather_conditions.json";
        var weatherConditions = await httpClient.GetFromJsonAsync<List<WeatherCondition>>(uri);

        return weatherConditions!;
    }

    private async Task<List<string>> GetExpectedWeatherConditions()
    {
        await using var fileStream = File.OpenRead("ExpectedWeather.json");
        var expectedWeatherConditions = await JsonSerializer.DeserializeAsync<List<string>>(fileStream);

        return expectedWeatherConditions!;
    }

    private class WeatherCondition
    {
        public int Code { get; set; }
        public string Day { get; set; } = string.Empty;
        public string Night { get; set; } = string.Empty;
        public int Icon { get; set; }
    }
}
