using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class attendance_record_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
              name: "FK_attendace_records_students_EStudentId",
              table: "attendace_records");

            migrationBuilder.DropForeignKey(
                name: "FK_attendace_records_students_StudentId",
                table: "attendace_records");

            migrationBuilder.DropIndex(
                name: "IX_attendace_records_EStudentId",
                table: "attendace_records");

            migrationBuilder.DropColumn(
                name: "EStudentId",
                table: "attendace_records")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "attendace_recordsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
            name: "IX_attendace_records_EStudentId",
            table: "attendace_records",
            column: "EStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendace_records_students_EStudentId",
                table: "attendace_records",
                column: "EStudentId",
                principalTable: "students",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_attendace_records_students_StudentId",
                table: "attendace_records",
                column: "StudentId",
                principalTable: "students",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
