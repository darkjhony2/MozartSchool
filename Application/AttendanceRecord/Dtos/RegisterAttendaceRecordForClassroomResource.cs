namespace ColegioMozart.Application.AttendanceRecord.Dtos;

public class RegisterAttendaceRecordForClassroomResource
{
    public Guid StudentId { get; set; }
    public int AttendanceStatusId { get; set; }
    public string Comments { get; set; }
}
