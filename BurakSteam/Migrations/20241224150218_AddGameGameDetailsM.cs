using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurakSteam.Migrations
{
    /// <inheritdoc />
    public partial class AddGameGameDetailsM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameDetails_GameId",
                table: "GameDetails");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetails_GameId",
                table: "GameDetails",
                column: "GameId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameDetails_GameId",
                table: "GameDetails");

            migrationBuilder.CreateIndex(
                name: "IX_GameDetails_GameId",
                table: "GameDetails",
                column: "GameId");
        }
    }
}
