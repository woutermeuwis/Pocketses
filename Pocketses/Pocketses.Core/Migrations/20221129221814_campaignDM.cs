using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocketses.Core.Migrations
{
    public partial class campaignDM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DungeonMasterId",
                table: "Campaigns",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_DungeonMasterId",
                table: "Campaigns",
                column: "DungeonMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_DungeonMasterId",
                table: "Campaigns",
                column: "DungeonMasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_DungeonMasterId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_DungeonMasterId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "DungeonMasterId",
                table: "Campaigns");
        }
    }
}
