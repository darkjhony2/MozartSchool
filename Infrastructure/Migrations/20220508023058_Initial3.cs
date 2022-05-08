using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_academic_levels_academic_levels_PreviousAcademicLevelId",
                table: "academic_levels");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousAcademicLevelId",
                table: "academic_levels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_academic_levels_academic_levels_PreviousAcademicLevelId",
                table: "academic_levels",
                column: "PreviousAcademicLevelId",
                principalTable: "academic_levels",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_academic_levels_academic_levels_PreviousAcademicLevelId",
                table: "academic_levels");

            migrationBuilder.AlterColumn<int>(
                name: "PreviousAcademicLevelId",
                table: "academic_levels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_academic_levels_academic_levels_PreviousAcademicLevelId",
                table: "academic_levels",
                column: "PreviousAcademicLevelId",
                principalTable: "academic_levels",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
