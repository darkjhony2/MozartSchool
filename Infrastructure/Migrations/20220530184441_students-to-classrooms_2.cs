using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class studentstoclassrooms_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classrooms_EClassRoomId",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "EClassRoomId",
                table: "students",
                newName: "ClassRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_students_EClassRoomId",
                table: "students",
                newName: "IX_students_ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classrooms_ClassRoomId",
                table: "students",
                column: "ClassRoomId",
                principalTable: "classrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classrooms_ClassRoomId",
                table: "students");

            migrationBuilder.RenameColumn(
                name: "ClassRoomId",
                table: "students",
                newName: "EClassRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_students_ClassRoomId",
                table: "students",
                newName: "IX_students_EClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classrooms_EClassRoomId",
                table: "students",
                column: "EClassRoomId",
                principalTable: "classrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
