using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pressure",
                table: "WeatherRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WindSpeed",
                table: "WeatherRecords",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "WeatherRecords");

            migrationBuilder.DropColumn(
                name: "WindSpeed",
                table: "WeatherRecords");
        }
    }
}
