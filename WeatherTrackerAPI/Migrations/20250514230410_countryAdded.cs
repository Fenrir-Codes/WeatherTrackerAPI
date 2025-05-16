using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class countryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "WeatherRecords");
        }
    }
}
