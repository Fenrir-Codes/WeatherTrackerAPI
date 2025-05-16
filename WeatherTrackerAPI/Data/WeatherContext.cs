using Microsoft.EntityFrameworkCore;
using WeatherTrackerAPI.Models;

namespace WeatherTrackerAPI.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options) { }

        public DbSet<WeatherData> WeatherRecords { get; set; }
    }
}
