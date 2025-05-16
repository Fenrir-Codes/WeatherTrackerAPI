using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class forecastAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ForecastDataId",
                table: "WeatherRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ForecastRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    Pressure = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_ForecastDataId",
                table: "WeatherRecords",
                column: "ForecastDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherRecords_ForecastRecords_ForecastDataId",
                table: "WeatherRecords",
                column: "ForecastDataId",
                principalTable: "ForecastRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherRecords_ForecastRecords_ForecastDataId",
                table: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "ForecastRecords");

            migrationBuilder.DropIndex(
                name: "IX_WeatherRecords_ForecastDataId",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "ForecastDataId",
                table: "WeatherRecords");
        }
    }
}
