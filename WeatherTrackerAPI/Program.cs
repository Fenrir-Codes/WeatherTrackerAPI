
using Microsoft.EntityFrameworkCore;
using WeatherTrackerAPI.Data;
using WeatherTrackerAPI.Services;

namespace WeatherTrackerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<WeatherContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TrackerConnection")));

            // Add services to the container.

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register HTTP client for WeatherService
            builder.Services.AddHttpClient<WeatherService>();
            // Register WeatherService
            builder.Services.AddScoped<WeatherService>();

            var app = builder.Build();

            // Enable CORS
            app.UseCors("AllowAll");
            app.UseCors("AllowSpecificOrigin");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
