using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherTrackerAPI.Data;
using WeatherTrackerAPI.Models;

namespace WeatherTrackerAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly WeatherContext _context;
        private readonly string ApiKey = "8bd4ace231927add489a91b0d775a625";

        public WeatherService(HttpClient httpClient, WeatherContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<(WeatherData? Data, string? ErrorMessage)> GetWeatherDataAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={ApiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var weatherResponse = JsonConvert.DeserializeObject<dynamic>(content);

                var weatherData = new WeatherData
                {
                    City = cityName,
                    Temperature = (int)Math.Round((double)weatherResponse.main.temp),
                    Humidity = (int)Math.Round((double)weatherResponse.main.humidity),
                    Pressure = (int)weatherResponse.main.pressure,
                    Icon = weatherResponse.weather[0].icon ?? "defaultIcon",
                    Country = weatherResponse.sys.country ?? "N/A",
                    WindSpeed = (int)Math.Round((double)weatherResponse.wind.speed),
                    Description = (string)weatherResponse.weather[0].description,
                    RetrievedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                return (weatherData, null);
            }
            catch (HttpRequestException ex)
            {
                return (null, $"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (null, $"Unexpected error: {ex.Message}");
            }
        }

        public async Task<(List<WeatherData>?, string?)> GetWeatherHistoryAsync()
        {
            try
            {
                var weatherData = await _context.WeatherRecords.ToListAsync();

                if (weatherData == null || weatherData.Count == 0)
                {
                    return (null, "No weather history found in the database.");
                }

                return (weatherData, null);
            }
            catch (Exception ex)
            {
                return (null, $"Error retrieving weather history: {ex.Message}");
            }
        }

        public async Task<(ForecastResponse?, string?)> GetWeatherForecastAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={ApiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(json);

                if (forecastResponse == null)
                    return (null, "Deserialization error!");

                return (forecastResponse, null);
            }
            catch (Exception ex)
            {
                return (null, $"Error: {ex.Message}");
            }
        }


    }
}
