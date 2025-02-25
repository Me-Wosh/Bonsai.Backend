namespace Bonsai.Backend.Services;

public interface IWeatherService
{
    Task<IResult> GetWeather(float latitude, float longitude);
}