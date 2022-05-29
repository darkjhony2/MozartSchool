using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class classroom1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_academic_levels_LevelId",
                table: "classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_sections_SectionId",
                table: "classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_shifts_ShiftId",
                table: "classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_teachers_TutorId",
                table: "classrooms");

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_academic_levels_LevelId",
                table: "classrooms",
                column: "LevelId",
                principalTable: "academic_levels",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_sections_SectionId",
                table: "classrooms",
                column: "SectionId",
                principalTable: "sections",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_shifts_ShiftId",
                table: "classrooms",
                column: "ShiftId",
                principalTable: "shifts",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_teachers_TutorId",
                table: "classrooms",
                column: "TutorId",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_academic_levels_LevelId",
                table: "classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_sections_SectionId",
                table: "classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_shifts_ShiftId",
                table: "classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_classrooms_teachers_TutorId",
                table: "classrooms");

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_academic_levels_LevelId",
                table: "classrooms",
                column: "LevelId",
                principalTable: "academic_levels",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_sections_SectionId",
                table: "classrooms",
                column: "SectionId",
                principalTable: "sections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_shifts_ShiftId",
                table: "classrooms",
                column: "ShiftId",
                principalTable: "shifts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_classrooms_teachers_TutorId",
                table: "classrooms",
                column: "TutorId",
                principalTable: "teachers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
