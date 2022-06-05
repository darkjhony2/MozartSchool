using ColegioMozart.Application.AttendanceStatus.Dtos;
using ColegioMozart.Application.Common.Mappings;
using ColegioMozart.Domain.Entities;

namespace ColegioMozart.Application.AttendanceRecord.Dtos;

public class AttendanceRecordForStudentDTO : IMapFrom<EAttendanceRecord>
{
    public DateOnly Date { get; set; }
    public AttendanceStatusDTO AttendanceStatus { get; set; }
    public string? Comments { get; set; }
}
