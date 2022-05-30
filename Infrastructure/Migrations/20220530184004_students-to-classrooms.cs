using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class studentstoclassrooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EClassRoomId",
                table: "students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_EClassRoomId",
                table: "students",
                column: "EClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_students_classrooms_EClassRoomId",
                table: "students",
                column: "EClassRoomId",
                principalTable: "classrooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_students_classrooms_EClassRoomId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_EClassRoomId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "EClassRoomId",
                table: "students")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "studentsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null);
        }
    }
}
