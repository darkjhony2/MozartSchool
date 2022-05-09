using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class Initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateEntityFullName",
                schema: "framework",
                table: "entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EditEntityFullName",
                schema: "framework",
                table: "entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateEntityFullName",
                schema: "framework",
                table: "entities");

            migrationBuilder.DropColumn(
                name: "EditEntityFullName",
                schema: "framework",
                table: "entities");
        }
    }
}
