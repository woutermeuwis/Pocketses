using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocketses.Core.Migrations
{
    public partial class usercampaigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_DungeonMasterId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Campaigns_CampaignId",
                table: "Characters");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignId",
                table: "Characters",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DungeonMasterId",
                table: "Campaigns",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CampaignUser",
                columns: table => new
                {
                    PlayersId = table.Column<string>(type: "TEXT", nullable: false),
                    UserCampaignsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignUser", x => new { x.PlayersId, x.UserCampaignsId });
                    table.ForeignKey(
                        name: "FK_CampaignUser_AspNetUsers_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignUser_Campaigns_UserCampaignsId",
                        column: x => x.UserCampaignsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignUser_UserCampaignsId",
                table: "CampaignUser",
                column: "UserCampaignsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_DungeonMasterId",
                table: "Campaigns",
                column: "DungeonMasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Campaigns_CampaignId",
                table: "Characters",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_AspNetUsers_DungeonMasterId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_AspNetUsers_UserId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Campaigns_CampaignId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "CampaignUser");

            migrationBuilder.DropIndex(
                name: "IX_Characters_UserId",
                table: "Characters");

            migrationBuilder.AlterColumn<Guid>(
                name: "CampaignId",
                table: "Characters",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "DungeonMasterId",
                table: "Campaigns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_AspNetUsers_DungeonMasterId",
                table: "Campaigns",
                column: "DungeonMasterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Campaigns_CampaignId",
                table: "Characters",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id");
        }
    }
}
