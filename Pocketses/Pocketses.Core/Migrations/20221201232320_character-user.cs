using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocketses.Core.Migrations
{
    public partial class characteruser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Characters",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Characters");
        }
    }
}
