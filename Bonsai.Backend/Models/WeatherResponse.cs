using System.Text.Json.Serialization;

namespace Bonsai.Backend.Models;

public class WeatherResponse
{
    public required Location Location { get; set; }
    public required Current Current { get; set; }
}

public class Condition
{
    public required string Text { get; set; }
}

public class Current
{
    public required Condition Condition { get; set; }
}

public class Location
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    [JsonPropertyName("lat")]
    public float Latitude { get; set; }
    [JsonPropertyName("lon")]
    public float Longitude { get; set; }
    [JsonPropertyName("tz_id")]
    public string TzId { get; set; } = string.Empty;
    [JsonPropertyName("localtime_epoch")]
    public ulong LocaltimeEpoch { get; set; }
    public string Localtime { get; set; } = string.Empty;
}