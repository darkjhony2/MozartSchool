using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityDtoFullName",
                schema: "framework",
                table: "entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EntityFullName",
                schema: "framework",
                table: "entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityDtoFullName",
                schema: "framework",
                table: "entities");

            migrationBuilder.DropColumn(
                name: "EntityFullName",
                schema: "framework",
                table: "entities");
        }
    }
}
