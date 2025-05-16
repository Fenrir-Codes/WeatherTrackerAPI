using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherTrackerAPI.Data;
using WeatherTrackerAPI.Services;

namespace WeatherTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherContext _context;
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var (weatherData, errorMessage) = await _weatherService.GetWeatherDataAsync(city);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }

            if (weatherData == null)
            {
                return NotFound(new { message = "City not found or data could not be retrieved." });
            }

            _context.WeatherRecords.Add(weatherData);
            await _context.SaveChangesAsync();

            return Ok(weatherData);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetWeatherHistory(int skip = 0, int take = 6)
        {
            var weatherRecords = await _context.WeatherRecords.OrderByDescending(w => w.RetrievedAt).Skip(skip).Take(take).ToListAsync();

            if (weatherRecords == null)
            {
                return NotFound(new { message = "No more weather history available." });
            }

            return Ok(weatherRecords);
        }

        [HttpGet("searchinhistory")]
        public async Task<IActionResult> SearchWeatherHistory(string? city)
        {
            var query = _context.WeatherRecords.AsQueryable();

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(w => w.City.ToLower().Contains(city.ToLower()));
            }

            var weatherRecords = await query
                .OrderByDescending(w => w.RetrievedAt)
                .ToListAsync();

            if (!weatherRecords.Any())
            {
                return NotFound(new { message = "No records found for the specified city." });
            }

            return Ok(weatherRecords);
        }


        [HttpGet("forecast")]
        public async Task<IActionResult> GetForecast(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("Please write the name of the city!");

            var (forecast, error) = await _weatherService.GetWeatherForecastAsync(city);

            if (error != null)
                return BadRequest(error);

            return Ok(forecast);
        }

    }

}
