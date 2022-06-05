using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class attendance_record_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "EStudentId",
              table: "attendace_records")
              .Annotation("SqlServer:IsTemporal", true)
              .Annotation("SqlServer:TemporalHistoryTableName", "attendace_recordsHistory")
              .Annotation("SqlServer:TemporalHistoryTableSchema", null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
