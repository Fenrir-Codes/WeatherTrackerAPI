using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class modelsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "icon",
                table: "WeatherRecords",
                newName: "Icon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "WeatherRecords",
                newName: "icon");
        }
    }
}
