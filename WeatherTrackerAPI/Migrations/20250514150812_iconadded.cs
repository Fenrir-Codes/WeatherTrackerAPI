using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class iconadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                table: "WeatherRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon",
                table: "WeatherRecords");
        }
    }
}
