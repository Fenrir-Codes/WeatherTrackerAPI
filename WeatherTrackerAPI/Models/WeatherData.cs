using Newtonsoft.Json;
using System.Globalization;

namespace WeatherTrackerAPI.Models
{
    public class WeatherData
    {
        public int Id { get; set; }
        public required string City { get; set; }
        public string? Country { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public int Pressure { get; set; }
        public string? Icon { get; set; }
        public string customIcon => string.Format("icon_{0}.png", Icon);
        public string? Description { get; set; }
        public string? RetrievedAt { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string? Main { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string CustomIcon => string.Format("icon_{0}.png", Icon);
    }

    public class ForecastResponse
    {
        public string? Cod { get; set; }
        public int Message { get; set; }
        public int Cnt { get; set; }
        public List<ForecastItem>? List { get; set; }

        public City? City { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

    public class ForecastItem
    {
        public int dt { get; set; }
        public string dateTime => UtcTimeLibrary.UtcTimeStamp.ConvertToUtc(dt);
        public Main? Main { get; set; }
        public List<Weather>? Weather { get; set; }
        public Clouds? Clouds { get; set; }
        public Wind? Wind { get; set; }
        public int Visibility { get; set; }
        public double Pop { get; set; }
        public Sys? Sys { get; set; }
        public string? Dt_txt { get; set; }
        public Rain? Rain { get; set; }
        public string? DayOfWeek => DateTime.TryParse(Dt_txt, out var date) ? date.ToString("dddd", CultureInfo.InvariantCulture) : null;
    }

    public class Main
    {
        public double temp { get; set; }
        public double temperature => Math.Round(temp);
        public double feels_like { get; set; }
        public string FeelsLike => Math.Round(feels_like).ToString("0");
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
        public int humidity { get; set; }
        public double temp_kf { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }

    public class Sys
    {
        public string? Pod { get; set; }
    }

    public class Rain
    {
        [JsonProperty("3h")]
        public double? VolumeLast3Hours { get; set; }
    }
}
