using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class attendance_record_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attendace_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendace_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attendace_records",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttendanceStatusId = table.Column<int>(type: "int", nullable: false),
                    AcademicPeriodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendace_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_attendace_records_academic_periods_AcademicPeriodId",
                        column: x => x.AcademicPeriodId,
                        principalTable: "academic_periods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_attendace_records_attendace_status_AttendanceStatusId",
                        column: x => x.AttendanceStatusId,
                        principalTable: "attendace_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_attendace_records_students_EStudentId",
                        column: x => x.EStudentId,
                        principalTable: "students",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_attendace_records_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "attendace_recordsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_attendace_records_AcademicPeriodId",
                table: "attendace_records",
                column: "AcademicPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_attendace_records_AttendanceStatusId",
                table: "attendace_records",
                column: "AttendanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_attendace_records_EStudentId",
                table: "attendace_records",
                column: "EStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_attendace_records_StudentId_Date",
                table: "attendace_records",
                columns: new[] { "StudentId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_attendace_status_Name",
                table: "attendace_status",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attendace_records")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "attendace_recordsHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "attendace_status");
        }
    }
}
