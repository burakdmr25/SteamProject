using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSteam.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoPathToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoPath",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoPath",
                table: "Games");
        }
    }
}
