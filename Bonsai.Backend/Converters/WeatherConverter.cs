using Bonsai.Backend.Models;

namespace Bonsai.Backend.Converters;

public static class WeatherConverter
{
    public static string Convert(string weather)
    {
        var keywords = new HashSet<string>(weather.Split(' ').Select(keyword => keyword.ToLower()));

        if (keywords.Contains("thunder") || 
            keywords.Contains("thundery"))
        {
            return WeatherType.Thundery;
        }

        if (keywords.Contains("partly") && 
            keywords.Contains("cloudy"))
        {
            return WeatherType.PartlyCloudy;
        }

        if (keywords.Contains("cloudy") || 
            keywords.Contains("fog")    || 
            keywords.Contains("mist")   || 
            keywords.Contains("overcast"))
        {
            return WeatherType.Cloudy;
        }

        if (keywords.Contains("drizzle") || 
            keywords.Contains("rain"))
        {
            return WeatherType.Rainy;
        }

        if (keywords.Contains("blizzard") || 
            keywords.Contains("pellets")  || 
            keywords.Contains("sleet")    || 
            keywords.Contains("snow"))
        {
            return WeatherType.Snowy;
        }

        if (keywords.Contains("clear") || 
            keywords.Contains("sunny"))
        {
            return WeatherType.Sunny;
        }

        throw new ArgumentException($"Unknown weather \"{weather}\"");
    }
}